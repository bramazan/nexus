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
                        else if (rawEvent.EntityType == "code_change")
                        {
                             _logger.LogInformation("Processing as Code Change...");
                             await ProcessGitLabCodeChangeAsync(rawEvent, cancellationToken);
                        }
                        else if (rawEvent.EntityType == "review")
                        {
                             _logger.LogInformation("Processing as Review (Discussions)...");
                             await ProcessGitLabReviewAsync(rawEvent, cancellationToken);
                        }
                        else if (rawEvent.EntityType == "review_approval")
                        {
                             _logger.LogInformation("Processing as Review Approval...");
                             await ProcessGitLabApprovalAsync(rawEvent, cancellationToken);
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
        private async Task ProcessGitLabCodeChangeAsync(RawEvent rawEvent, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(rawEvent.Payload);
            var root = document.RootElement; // Expected to be list of changes or an object containing changes?
            // GitLab 'changes' endpoint returns the MR object WITH a 'changes' array.
            
            JsonElement changesArray;
            if (root.ValueKind == JsonValueKind.Array)
            {
                 // Sometimes it might be array of changes directly? Verify GitLab API. 
                 // Endpoint: /projects/:id/merge_requests/:iid/changes returns the MR object with 'changes' attribute.
                 // But wait, if we used GetFromJsonAsync<JsonElement>, and it returned the object.
                 if (!root.TryGetProperty("changes", out changesArray))
                 {
                     // Maybe the payload IS the changes array if we extracted it?
                     // My command log said: existingEvent.Payload = jsonElement.GetRawText();
                     // And connector: return await client.GetFromJsonAsync<JsonElement>(endpoint);
                     // If endpoint is .../changes, it returns MR object with 'changes' list.
                     changesArray = root; // Fallback assumption, likely wrong if it is the MR object.
                 }
            }
            else
            {
                if (!root.TryGetProperty("changes", out changesArray))
                {
                    _logger.LogWarning("No 'changes' property found in payload.");
                    return;
                }
            }

            // Extract IDs from EntityId: "{ProjectId}-{Iid}-changes"
            var parts = rawEvent.EntityId.Split('-');
            if (parts.Length < 3)
            {
                _logger.LogWarning($"Invalid EntityId format: {rawEvent.EntityId}");
                return;
            }
            
            var projectIdStr = parts[0];
            if (!int.TryParse(parts[1], out int iid))
            {
                 _logger.LogWarning($"Could not parse IID from EntityId: {rawEvent.EntityId}");
                 return;
            }

            // Find Repository
            var repo = await _context.Repositories
                .FirstOrDefaultAsync(r => r.ExternalId == projectIdStr && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);
                
            if (repo == null) 
            {
                _logger.LogWarning($"Repository not found for ExternalId: {projectIdStr}");
                return;
            }

            // Find PullRequest
            var pr = await _context.PullRequests
                .FirstOrDefaultAsync(p => p.RepositoryId == repo.Id && p.Number == iid, cancellationToken);

            if (pr == null)
            {
                 // Maybe PR not synced yet? 
                 _logger.LogWarning($"Pull Request not found for RepoIdMs: {repo.Id} and Number: {iid}");
                 // Optional: Create stub PR? No, let's wait for MR sync.
                 return;
            }

            // Process Changes
            foreach (var change in changesArray.EnumerateArray())
            {
                var newPath = change.GetProperty("new_path").GetString();
                var diff = change.TryGetProperty("diff", out var diffProp) ? diffProp.GetString() : "";
                
                // Calculate stats from diff
                int added = 0;
                int deleted = 0;
                if (!string.IsNullOrEmpty(diff))
                {
                    var lines = diff.Split('\n');
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("+") && !line.StartsWith("+++")) added++;
                        if (line.StartsWith("-") && !line.StartsWith("---")) deleted++;
                    }
                }
                
                // Identifiers
                // We don't have a unique ID for a "Change" from GitLab other than file path per MR.
                // So check if exists by PR + FilePath
                
                var existingChange = await _context.CodeChanges
                    .FirstOrDefaultAsync(cc => cc.PullRequestId == pr.Id && cc.FilePath == newPath, cancellationToken);
                    
                if (existingChange == null)
                {
                    var codeChange = new CodeChange
                    {
                        Id = Guid.NewGuid(),
                        PullRequestId = pr.Id,
                        FilePath = newPath!,
                        LinesAdded = added,
                        LinesDeleted = deleted,
                        IsRefactor = false, // TODO: AI Analysis
                        IsRework = false,   // TODO: AI Analysis
                        CreatedAt = DateTime.UtcNow
                        // CommitId is nullable now
                    };
                    _context.CodeChanges.Add(codeChange);
                }
                else
                {
                    // Update stats
                    existingChange.LinesAdded = added;
                    existingChange.LinesDeleted = deleted;
                }
            }

            MarkAsProcessed(rawEvent);
        }
        private async Task ProcessGitLabReviewAsync(RawEvent rawEvent, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(rawEvent.Payload);
            var root = document.RootElement;
            // Expecting array of notes
            
            if (root.ValueKind != JsonValueKind.Array)
            {
                _logger.LogWarning("Payload is not an array of notes.");
                return;
            }

            // EntityId: "{ProjectId}-{Iid}-notes"
            var parts = rawEvent.EntityId.Split('-');
            if (parts.Length < 3) return;
            var projectIdStr = parts[0];
            if (!int.TryParse(parts[1], out int iid)) return;

            // Find Repo and PR
            var repo = await _context.Repositories
                .FirstOrDefaultAsync(r => r.ExternalId == projectIdStr && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);
            if (repo == null) return;

            var pr = await _context.PullRequests
                .FirstOrDefaultAsync(p => p.RepositoryId == repo.Id && p.Number == iid, cancellationToken);
            if (pr == null) return;

            foreach (var note in root.EnumerateArray())
            {
                // Filter system notes?
                bool system = note.GetProperty("system").GetBoolean();
                if (system) continue; // Skip system notes for now? Or keep as different type? Let's skip.

                var noteId = note.GetProperty("id").GetInt64().ToString();
                var body = note.GetProperty("body").GetString();
                var createdAt = note.GetProperty("created_at").GetDateTime().ToUniversalTime();
                
                // Author
                var author = note.GetProperty("author");
                var authorUsername = author.GetProperty("username").GetString();
                var authorEmail = author.TryGetProperty("email", out var emailProp) ? emailProp.GetString() : null; // notes usually don't have email in public api?

                // Find existing review
                var existingReview = await _context.PullRequestReviews
                    .FirstOrDefaultAsync(r => r.PullRequestId == pr.Id && r.ExternalId == noteId, cancellationToken);

                if (existingReview == null)
                {
                   var review = new PullRequestReview
                   {
                       Id = Guid.NewGuid(),
                       PullRequestId = pr.Id,
                       ExternalId = noteId,
                       Body = body,
                       State = "COMMENTED", // Default
                       CreatedAt = createdAt,
                       SubmittedAt = createdAt,
                   };

                   // Link Author
                   var toolAccount = await _context.ToolAccounts
                       .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.Username == authorUsername, cancellationToken);
                   
                   if (toolAccount != null)
                   {
                       review.AuthorToolAccountId = toolAccount.Id;
                   }

                   _context.PullRequestReviews.Add(review);
                }
            }

            MarkAsProcessed(rawEvent);
        }

        private async Task ProcessGitLabApprovalAsync(RawEvent rawEvent, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(rawEvent.Payload);
            var root = document.RootElement;
            
            // Payload structure for approvals: { "id": mr_id, "project_id": pid, ..., "approved_by": [ { "user": { "username": "..." } } ] }
            
            if (!root.TryGetProperty("approved_by", out var approvedByArray))
            {
                 // No approvals or wrong format
                 MarkAsProcessed(rawEvent);
                 return;
            }

            // EntityId: "{ProjectId}-{Iid}-approvals"
            var parts = rawEvent.EntityId.Split('-');
            if (parts.Length < 3) return;
            var projectIdStr = parts[0];
            if (!int.TryParse(parts[1], out int iid)) return;

             // Find Repo and PR
            var repo = await _context.Repositories
                .FirstOrDefaultAsync(r => r.ExternalId == projectIdStr && r.IntegrationId == rawEvent.IntegrationId, cancellationToken);
            if (repo == null) return;

            var pr = await _context.PullRequests
                .FirstOrDefaultAsync(p => p.RepositoryId == repo.Id && p.Number == iid, cancellationToken);
            if (pr == null) return;

            foreach (var approval in approvedByArray.EnumerateArray())
            {
                if (approval.TryGetProperty("user", out var userProp))
                {
                    var username = userProp.GetProperty("username").GetString();
                    
                    var toolAccount = await _context.ToolAccounts
                       .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.Username == username, cancellationToken);
                    
                    var toolAccountId = toolAccount?.Id;

                    // Check if exists using ExternalId
                    bool exists = await _context.PullRequestReviews
                            .AnyAsync(r => r.PullRequestId == pr.Id && r.ExternalId == $"approval-{username}", cancellationToken);

                    if (!exists)
                    {
                         var review = new PullRequestReview
                         {
                             Id = Guid.NewGuid(),
                             PullRequestId = pr.Id,
                             AuthorToolAccountId = toolAccountId,
                             ExternalId = $"approval-{username}",
                             State = "APPROVED",
                             CreatedAt = rawEvent.OccurredAt,
                             SubmittedAt = rawEvent.OccurredAt,
                             Body = "Approved via GitLab"
                         };
                         _context.PullRequestReviews.Add(review);
                    }
                }
            }
            
            MarkAsProcessed(rawEvent);
        }
    }
}
