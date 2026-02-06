using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.Jira.Commands
{
    public record SyncJiraIssuesCommand(Guid IntegrationId) : IRequest<int>;

    public class SyncJiraIssuesCommandHandler : IRequestHandler<SyncJiraIssuesCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJiraConnector _jiraConnector;

        public SyncJiraIssuesCommandHandler(IApplicationDbContext context, IJiraConnector jiraConnector)
        {
            _context = context;
            _jiraConnector = jiraConnector;
        }

        public async Task<int> Handle(SyncJiraIssuesCommand request, CancellationToken cancellationToken)
        {
            // Fetch updated issues in the last 24 hours (for incremental sync, or similar strategy)
            // For now, let's sync all updated recently
            string jql = "updated >= -30d ORDER BY updated DESC"; 
            var result = await _jiraConnector.GetIssuesAsync(request.IntegrationId, jql, 0, 100); 
            int count = 0;

            foreach (var jiraIssue in result.Issues)
            {
                // Fetch full details including changelog
                var issueDetails = await _jiraConnector.GetIssueDetailsAsync(request.IntegrationId, jiraIssue.Key);

                var issue = await _context.Issues
                    .Include(i => i.Events)
                    .Include(i => i.SprintLinks) // Include links to update them
                    .FirstOrDefaultAsync(i => i.IntegrationId == request.IntegrationId && i.ExternalId == issueDetails.Key, cancellationToken);

                if (issue == null)
                {
                    issue = new Issue
                    {
                        IntegrationId = request.IntegrationId,
                        ExternalId = issueDetails.Key,
                        Title = issueDetails.Fields.Summary,
                        Status = issueDetails.Fields.Status.Name,
                        Type = issueDetails.Fields.IssueType.Name,
                        Priority = issueDetails.Fields.Priority?.Name,
                        // TODO: Map Assignee/Reporter to Users if possible
                        CreatedBy = "System", // Placeholder
                    };
                    _context.Issues.Add(issue);
                }
                else
                {
                    issue.Title = issueDetails.Fields.Summary;
                    issue.Status = issueDetails.Fields.Status.Name;
                    issue.Type = issueDetails.Fields.IssueType.Name;
                    issue.Priority = issueDetails.Fields.Priority?.Name;
                }

                // Process Changelog (Issue Events)
                foreach (var history in issueDetails.Changelog.Histories)
                {
                    foreach (var item in history.Items)
                    {
                        // Check if event already exists (deduplication based on time and field)
                        var exists = issue.Events.Any(e => e.OccurredAt == history.Created && e.EventType == item.Field);
                        if (!exists)
                        {
                            issue.Events.Add(new IssueEvent
                            {
                                EventType = item.Field,
                                PreviousValue = item.FromString,
                                NewValue = item.ToStringValue,
                                OccurredAt = history.Created.ToUniversalTime(),
                                CreatedBy = "System"
                            });
                        }
                    }
                }

                // Process Custom Fields (e.g., Sprints)
                // Note: Sprint field ID varies by instance. Usually "customfield_10020" or similar.
                // Since DTO uses generic Dict, we iterate to find List<JsonElement> that looks like sprint data.
                if (issueDetails.Fields.CustomFields != null)
                {
                    foreach (var field in issueDetails.Fields.CustomFields)
                    {
                        if (field.Value.ValueKind == JsonValueKind.Array)
                        {
                            // Heuristic check for sprint data string like "com.atlassian.greenhopper.service.sprint.Sprint@..."
                            // Or object that contains "id", "name", "state"
                        }
                    }
                }
                
                // Save initially to get Issue Id for links
                // However, we are in a loop, optimization would use batching. 
                // For simplicity in this command, we add to context and save at end or per batch.
                count++;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return count;
        }
    }
}
