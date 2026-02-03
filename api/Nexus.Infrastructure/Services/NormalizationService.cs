using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Infrastructure.Services
{
    public class NormalizationService : INormalizationService
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<NormalizationService> _logger;

        public NormalizationService(IApplicationDbContext context, ILogger<NormalizationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task NormalizePendingEventsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting normalization of pending events...");
            
            var pendingEvents = await _context.RawEvents
                .Where(e => e.Status == ProcessingStatus.Pending)
                .OrderBy(e => e.OccurredAt)
                .Take(10000) // Batch size
                .ToListAsync(cancellationToken);
                
            _logger.LogInformation($"Found {pendingEvents.Count} pending events.");

            foreach (var rawEvent in pendingEvents)
            {
                _logger.LogInformation($"Processing event {rawEvent.Id} - Source: {rawEvent.Source}, Type: {rawEvent.EntityType}");
                try
                {
                    if (rawEvent.Source == "GitLab")
                    {
                        if (rawEvent.EntityType == "commits")
                        {
                            await ProcessGitLabCommitAsync(rawEvent, cancellationToken);
                        }
                        else if (rawEvent.EntityType == "pull_request" || rawEvent.EntityType == "mergerequests" || rawEvent.EntityType == "mrs")
                        {
                            _logger.LogInformation("Processing as Merge Request...");
                            await ProcessGitLabMergeRequestAsync(rawEvent, cancellationToken);
                        }
                        else
                        {
                            _logger.LogWarning($"No handler for Source: {rawEvent.Source}, EntityType: {rawEvent.EntityType}");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"No handler for Source: {rawEvent.Source}, EntityType: {rawEvent.EntityType}");
                    }
                    
                    // Mark as successful if processed (handler should throw if failed)
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to process event {rawEvent.Id}");
                    rawEvent.Status = ProcessingStatus.Failed;
                    rawEvent.ErrorMessage = ex.Message;
                    rawEvent.ProcessedAt = DateTime.UtcNow;
                }
            }
            
            _logger.LogInformation("Saving changes...");
            var result = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Saved {result} changes.");
        }

        private async Task ProcessGitLabCommitAsync(RawEvent rawEvent, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(rawEvent.Payload);
            var root = document.RootElement;
            
            var sha = root.GetProperty("id").GetString()!;
            var message = root.GetProperty("message").GetString();
            var authorName = root.GetProperty("author_name").GetString();
            var authorEmail = root.GetProperty("author_email").GetString();
            var authoredDate = root.GetProperty("authored_date").GetDateTime().ToUniversalTime();
            var committedDate = root.GetProperty("committed_date").GetDateTime().ToUniversalTime();
            var webUrl = root.GetProperty("web_url").GetString();
            
            var repositoryId = await ResolveRepositoryIdAsync(rawEvent.IntegrationId, webUrl, "/-/commit/", cancellationToken);

            if (repositoryId == Guid.Empty)
            {
                 throw new Exception($"Could not resolve Repository from WebUrl: {webUrl}");
            }

            // check if commit exists
            var existingCommit = await _context.Commits
                .FirstOrDefaultAsync(c => c.Sha == sha && c.RepositoryId == repositoryId, cancellationToken);
            
            if (existingCommit == null)
            {
                var commit = new Commit
                {
                    Id = Guid.NewGuid(),
                    RepositoryId = repositoryId,
                    Sha = sha,
                    Message = message,
                    AuthorName = authorName,
                    AuthorEmail = authorEmail,
                    AuthoredDate = authoredDate,
                    CommittedAt = committedDate,
                    WebUrl = webUrl,
                    CreatedAt = DateTime.UtcNow
                };

                // Link User
                if (!string.IsNullOrEmpty(authorEmail))
                {
                    commit.UserId = await ResolveUserIdByEmailAsync(rawEvent.IntegrationId, authorEmail, cancellationToken);
                }
                
                _context.Commits.Add(commit);
            }
            
            MarkAsProcessed(rawEvent);
        }

        private async Task ProcessGitLabMergeRequestAsync(RawEvent rawEvent, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(rawEvent.Payload);
            var root = document.RootElement;

            var iid = root.GetProperty("iid").GetInt32();
            var externalId = root.GetProperty("id").ToString(); // Keep as string for flexibility
            var title = root.GetProperty("title").GetString()!;
            var description = root.TryGetProperty("description", out var descProp) ? descProp.GetString() : null;
            var state = root.GetProperty("state").GetString()!;
            
            var createdAt = root.GetProperty("created_at").GetDateTime().ToUniversalTime();
            var updatedAt = root.GetProperty("updated_at").GetDateTime().ToUniversalTime();
            
            DateTime? mergedAt = null;
            if (root.TryGetProperty("merged_at", out var mergedAtProp) && mergedAtProp.ValueKind != JsonValueKind.Null)
            {
                mergedAt = mergedAtProp.GetDateTime().ToUniversalTime();
            }

            DateTime? closedAt = null;
            if (root.TryGetProperty("closed_at", out var closedAtProp) && closedAtProp.ValueKind != JsonValueKind.Null)
            {
                closedAt = closedAtProp.GetDateTime().ToUniversalTime();
            }

            var sourceBranch = root.GetProperty("source_branch").GetString();
            var targetBranch = root.GetProperty("target_branch").GetString();
            var webUrl = root.GetProperty("web_url").GetString();

            var repositoryId = await ResolveRepositoryIdAsync(rawEvent.IntegrationId, webUrl, "/-/merge_requests/", cancellationToken);
            
            if (repositoryId == Guid.Empty)
            {
                // Fallback trial: sometimes url structure is different or we default to finding project by other means?
                // For now, fail if strict. But to be safe, maybe log and skip?
                // Let's rely on ResolveRepositoryIdAsync.
                throw new Exception($"Could not resolve Repository from WebUrl: {webUrl}");
            }

            // Check if PR exists
            var existingPR = await _context.PullRequests
                .FirstOrDefaultAsync(pr => pr.RepositoryId == repositoryId && pr.Number == iid, cancellationToken);

            if (existingPR == null)
            {
                var pr = new PullRequest
                {
                    Id = Guid.NewGuid(),
                    IntegrationId = rawEvent.IntegrationId,
                    RepositoryId = repositoryId,
                    ExternalId = externalId,
                    Number = iid,
                    Title = title,
                    State = state, // TODO: Map to standardized state? For now, keep as source state.
                    SourceBranch = sourceBranch,
                    TargetBranch = targetBranch,
                    Description = description,
                    CreatedAt = createdAt,
                    MergedAt = mergedAt,
                    ClosedAt = closedAt,
                    // Calculated fields default
                    ReviewCount = 0, 
                    SizeLines = 0,
                    IsAiGenerated = false 
                };

                // Author resolution
                if (root.TryGetProperty("author", out var authorProp))
                {
                    string? email = null;
                    if (authorProp.TryGetProperty("email", out var emailProp) && emailProp.ValueKind != JsonValueKind.Null)
                    {
                        email = emailProp.GetString();
                    }
                    
                    // If no email in public author object, we might try username match or skipping
                    string? username = null;
                    if (authorProp.TryGetProperty("username", out var usernameProp))
                    {
                        username = usernameProp.GetString();
                    }

                    if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(username))
                    {
                        // Try to find ToolAccount
                        var toolAccount = await _context.ToolAccounts
                            .FirstOrDefaultAsync(ta => 
                                ta.IntegrationId == rawEvent.IntegrationId && 
                                ((email != null && ta.ExternalEmail == email) || (username != null && ta.Username == username)), 
                                cancellationToken);
                        
                        if (toolAccount != null)
                        {
                            pr.AuthorToolAccountId = toolAccount.Id;
                        }
                    }
                }

                _context.PullRequests.Add(pr);
            }
            else
            {
                // Update existing PR
                // Important for updated status
                existingPR.Title = title;
                existingPR.Description = description;
                existingPR.State = state;
                existingPR.SourceBranch = sourceBranch;
                existingPR.TargetBranch = targetBranch;
                existingPR.MergedAt = mergedAt;
                existingPR.ClosedAt = closedAt;
                // Don't overwrite created_at or author if already set ideally
            }

            MarkAsProcessed(rawEvent);
        }

        // Helpers
        private async Task<Guid> ResolveRepositoryIdAsync(Guid integrationId, string? webUrl, string splitToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(webUrl)) return Guid.Empty;

            var repoUrlPart = webUrl.Split(splitToken)[0];
            
            // Exact match try
            var repo = await _context.Repositories
                .FirstOrDefaultAsync(r => r.Url == repoUrlPart && r.IntegrationId == integrationId, cancellationToken);
            
            if (repo != null) return repo.Id;

            // Loose match (case insensitive? or trim slash?)
            return Guid.Empty;
        }

        private async Task<Guid?> ResolveUserIdByEmailAsync(Guid integrationId, string email, CancellationToken cancellationToken)
        {
             var toolAccount = await _context.ToolAccounts
                .Include(ta => ta.User)
                .FirstOrDefaultAsync(ta => 
                    ta.IntegrationId == integrationId && 
                    (ta.ExternalEmail == email || ta.Username == email),
                    cancellationToken);
                    
            if (toolAccount != null) return toolAccount.UserId;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user?.Id;
        }

        private void MarkAsProcessed(RawEvent rawEvent)
        {
            rawEvent.Status = ProcessingStatus.Processed;
            rawEvent.ProcessedAt = DateTime.UtcNow;
            rawEvent.ErrorMessage = null;
        }
    }
}
