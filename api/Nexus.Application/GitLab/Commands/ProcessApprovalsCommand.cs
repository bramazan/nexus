using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessApprovalsCommand : IRequest<int>;

    public class ProcessApprovalsCommandHandler : IRequestHandler<ProcessApprovalsCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessApprovalsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessApprovalsCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => e.EntityType == "review_approval" && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    // EntityId: "{ProjectId}-{Iid}"
                    // Payload: { "id": 5, "iid": 5, "project_id": 12, "title": "...", "approved_by": [ { "user": { "id": 1, ... } } ] }
                    // OR Payload is a list of approvals?
                    // Usually "approvals" endpoint returns single object with "approved_by" list.
                    
                    using (JsonDocument doc = JsonDocument.Parse(rawEvent.Payload))
                    {
                        var root = doc.RootElement;
                        // Locate MR
                        var parts = rawEvent.EntityId.Split('-');
                        if (parts.Length < 2)
                        {
                             rawEvent.ProcessedAt = DateTime.UtcNow;
                             rawEvent.Status = ProcessingStatus.Failed;
                             rawEvent.ErrorMessage = "Invalid EntityId";
                             continue;
                        }

                        var projectId = parts[0];
                        var iid = int.Parse(parts[1]);

                        // Resolve Repository
                        var repository = await _context.Repositories
                            .FirstOrDefaultAsync(r => r.IntegrationId == rawEvent.IntegrationId && r.ExternalId == projectId, cancellationToken);
                        
                        if (repository == null)
                        {
                            rawEvent.ProcessedAt = DateTime.UtcNow;
                            rawEvent.Status = ProcessingStatus.Failed;
                            rawEvent.ErrorMessage = "Repository not found";
                            continue;
                        }

                        var pr = await _context.PullRequests
                            .FirstOrDefaultAsync(p => p.RepositoryId == repository.Id && p.Number == iid, cancellationToken);

                        if (pr == null)
                        {
                             // MR not synced yet or deleted?
                             rawEvent.ProcessedAt = DateTime.UtcNow;
                             rawEvent.Status = ProcessingStatus.Failed;
                             rawEvent.ErrorMessage = "Pull Request not found";
                             continue;
                        }

                        // Process "approved_by"
                        if (root.TryGetProperty("approved_by", out var approvedByList))
                        {
                            foreach (var item in approvedByList.EnumerateArray())
                            {
                                if (item.TryGetProperty("user", out var userObj))
                                {
                                    int userId = userObj.GetProperty("id").GetInt32();
                                    string username = userObj.GetProperty("username").GetString() ?? "";
                                    
                                    // Handle ToolAccount
                                    var toolAccount = await _context.ToolAccounts
                                        .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == userId.ToString(), cancellationToken);
                                    
                                    if (toolAccount == null)
                                    {
                                         // Create account on fly?
                                        var user = await _context.Users.FirstOrDefaultAsync(u => u.FullName == username, cancellationToken); // Simple match
                                        if (user == null)
                                        {
                                            user = new User
                                            {
                                                FullName = username,
                                                Email = $"{username}@gitlab.nexus.placeholder"
                                            };
                                            _context.Users.Add(user);
                                            await _context.SaveChangesAsync(cancellationToken);
                                        }

                                        toolAccount = new ToolAccount
                                        {
                                            UserId = user.Id,
                                            IntegrationId = rawEvent.IntegrationId,
                                            ExternalId = userId.ToString(),
                                            Username = username,
                                            DisplayName = username, // Name prop?
                                            IsActive = true
                                        };
                                        _context.ToolAccounts.Add(toolAccount);
                                        await _context.SaveChangesAsync(cancellationToken);
                                    }

                                    // Create PullRequestReview (State = Approved)
                                    // Check if exists
                                    var existingReview = await _context.PullRequestReviews
                                        .FirstOrDefaultAsync(r => r.PullRequestId == pr.Id && r.AuthorToolAccountId == toolAccount.Id && r.State == "approved", cancellationToken);
                                    
                                    if (existingReview == null)
                                    {
                                        existingReview = new PullRequestReview
                                        {
                                            PullRequestId = pr.Id,
                                            AuthorToolAccountId = toolAccount.Id,
                                            State = "approved",
                                            SubmittedAt = rawEvent.OccurredAt // Or now?
                                        };
                                        _context.PullRequestReviews.Add(existingReview);
                                    }
                                }
                            }
                        }
                    }

                    rawEvent.ProcessedAt = DateTime.UtcNow;
                    rawEvent.Status = ProcessingStatus.Processed;
                    processedCount++;
                }
                catch (Exception ex)
                {
                    rawEvent.ProcessedAt = DateTime.UtcNow;
                    rawEvent.Status = ProcessingStatus.Failed;
                    rawEvent.ErrorMessage = ex.Message;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return processedCount;
        }
    }
}
