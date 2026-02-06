using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record ProcessIssuesCommand : IRequest<int>;

    public class ProcessIssuesCommandHandler : IRequestHandler<ProcessIssuesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ProcessIssuesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ProcessIssuesCommand request, CancellationToken cancellationToken)
        {
            var pendingEvents = await _context.RawEvents
                .Where(e => (e.EntityType == "issue" || e.EntityType == "issues") && e.ProcessedAt == null)
                .OrderBy(e => e.OccurredAt)
                .Take(1000)
                .ToListAsync(cancellationToken);

            int processedCount = 0;

            foreach (var rawEvent in pendingEvents)
            {
                try
                {
                    var gitLabIssue = JsonSerializer.Deserialize<GitLabIssue>(rawEvent.Payload);
                    if (gitLabIssue == null)
                    {
                        rawEvent.ProcessedAt = DateTime.UtcNow;
                        rawEvent.Status = ProcessingStatus.Failed;
                        rawEvent.ErrorMessage = "Payload deserialization failed.";
                        continue;
                    }

                    // Check existence
                    // ExternalId should be unique per Integration? Or Project?
                    // Issue entity has ExternalId.
                    // Let's assume unique per Integration + ExternalId.
                    
                    var issue = await _context.Issues
                        .FirstOrDefaultAsync(i => i.IntegrationId == rawEvent.IntegrationId && i.ExternalId == gitLabIssue.Id.ToString(), cancellationToken);

                    if (issue == null)
                    {
                        issue = new Issue
                        {
                            IntegrationId = rawEvent.IntegrationId,
                            ExternalId = gitLabIssue.Id.ToString(),
                            Title = gitLabIssue.Title,
                            Description = gitLabIssue.Description,
                            Status = gitLabIssue.State, // opened, closed
                            Type = "issue", // Default type
                            StartedAt = gitLabIssue.CreatedAt.ToUniversalTime(),
                            CompletedAt = gitLabIssue.ClosedAt?.ToUniversalTime()
                        };
                        _context.Issues.Add(issue);
                        
                        // Handle Assignees (List)
                        if (gitLabIssue.Assignees != null && gitLabIssue.Assignees.Any())
                        {
                            foreach (var assignee in gitLabIssue.Assignees)
                            {
                                 var toolAccount = await _context.ToolAccounts
                                    .FirstOrDefaultAsync(ta => ta.IntegrationId == rawEvent.IntegrationId && ta.ExternalId == assignee.Id.ToString(), cancellationToken);
                                
                                 if (toolAccount == null)
                                 {
                                    // Create on fly
                                    var user = await _context.Users.FirstOrDefaultAsync(u => u.FullName == assignee.Name, cancellationToken);
                                    if (user == null)
                                    {
                                        user = new User
                                        {
                                            FullName = assignee.Name,
                                            Email = $"{assignee.Username}@gitlab.nexus.placeholder"
                                        };
                                        _context.Users.Add(user);
                                        await _context.SaveChangesAsync(cancellationToken);
                                    }
                                    toolAccount = new ToolAccount
                                    {
                                        UserId = user.Id,
                                        IntegrationId = rawEvent.IntegrationId,
                                        ExternalId = assignee.Id.ToString(),
                                        Username = assignee.Username,
                                        DisplayName = assignee.Name,
                                        IsActive = assignee.State == "active"
                                    };
                                    _context.ToolAccounts.Add(toolAccount);
                                    await _context.SaveChangesAsync(cancellationToken);
                                 }

                                 // Check duplicates
                                 if (!issue.Assignees.Any(a => a.UserId == toolAccount.UserId))
                                 {
                                     issue.Assignees.Add(new IssueAssignee 
                                     { 
                                         UserId = toolAccount.UserId,
                                         AssignedAt = DateTime.UtcNow 
                                     });
                                 }
                            }
                        }
                    }
                    else
                    {
                        issue.Title = gitLabIssue.Title;
                        issue.Description = gitLabIssue.Description;
                        issue.Status = gitLabIssue.State;
                        issue.CompletedAt = gitLabIssue.ClosedAt?.ToUniversalTime();
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
