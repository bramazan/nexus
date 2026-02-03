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
            var pendingEvents = await _context.RawEvents
                .Where(e => e.Status == ProcessingStatus.Pending)
                .OrderBy(e => e.OccurredAt)
                .Take(50) // Batch size
                .ToListAsync(cancellationToken);

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    if (rawEvent.Source == "GitLab" && rawEvent.EntityType == "commits")
                    {
                        await ProcessGitLabCommitAsync(rawEvent, cancellationToken);
                    }
                    else
                    {
                        // Skip unknown types for now, or mark as failed? 
                        // Let's mark as Processed but log that we didn't do anything specific, or just leave it?
                        // Better to leave it or implement handlers. For now, just skip.
                        _logger.LogWarning($"No handler for Source: {rawEvent.Source}, EntityType: {rawEvent.EntityType}");
                    }
                    
                    // Mark as successful if processed (handler should throw if failed)
                    // If handler logic is void, we assume success or internal catch.
                }
                catch (Exception ex)
                {
                    rawEvent.Status = ProcessingStatus.Failed;
                    rawEvent.ErrorMessage = ex.Message;
                    rawEvent.ProcessedAt = DateTime.UtcNow;
                    _logger.LogError(ex, $"Failed to normalize event {rawEvent.Id}");
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
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

            // Try to resolve Repository
            // Strategy: Extract potential repo URL from web_url or external_id?
            // User example WebUrl: https://git.odeal.com/odeal/finance/transfer/-/commit/04cb8b2e...
            // Repo URL in DB might be: https://git.odeal.com/odeal/finance/transfer
            
            Guid repositoryId = Guid.Empty;
            
            // Simplistic URL matching
            if (!string.IsNullOrEmpty(webUrl))
            {
                // Remove /-/commit/... part
                var repoUrlPart = webUrl.Split("/-/commit/")[0];
                var repo = await _context.Repositories
                    .FirstOrDefaultAsync(r => r.Url == repoUrlPart && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);
                
                if (repo != null)
                {
                    repositoryId = repo.Id;
                }
            }
            
            // If repository not found, we might want to create it or fail? 
            // For now, let's fail if repository is strict requirements, else we can't save foreign key.
            // But wait, RepositoryId is GUID in Commit entity. Is it nullable?
            // In Commit.cs: public Guid RepositoryId { get; set; } -> NOT nullable.
            // So we MUST find a repo.
            
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
                    var toolAccount = await _context.ToolAccounts
                        .Include(ta => ta.User)
                        .FirstOrDefaultAsync(ta => 
                            ta.IntegrationId == rawEvent.IntegrationId && 
                            (ta.ExternalEmail == authorEmail || ta.Username == authorEmail), // Simple match
                            cancellationToken);
                            
                    if (toolAccount != null)
                    {
                        commit.UserId = toolAccount.UserId;
                    }
                    else
                    {
                        // Fallback: Try to find User by email directly
                        var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.Email == authorEmail, cancellationToken);
                            
                        if (user != null)
                        {
                            commit.UserId = user.Id;
                        }
                    }
                }
                
                _context.Commits.Add(commit);
            }
            
            rawEvent.Status = ProcessingStatus.Processed;
            rawEvent.ProcessedAt = DateTime.UtcNow;
            rawEvent.ErrorMessage = null;
        }
    }
}
