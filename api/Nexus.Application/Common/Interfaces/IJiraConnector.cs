using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexus.Application.Common.Interfaces
{
    public interface IJiraConnector
    {
        // Core Resources
        Task<IList<Nexus.Application.Common.Models.JiraProject>> GetProjectsAsync(Guid integrationId);
        Task<Nexus.Application.Common.Models.JiraIssueSearchResponse> GetIssuesAsync(Guid integrationId, string jql, int startAt = 0, int maxResults = 50);
        
        // Extended Resources for Agile Metrics
        Task<IList<Nexus.Application.Common.Models.JiraBoard>> GetBoardsAsync(Guid integrationId);
        Task<IList<Nexus.Application.Common.Models.JiraSprint>> GetSprintsAsync(Guid integrationId, int boardId);
        Task<Nexus.Application.Common.Models.JiraIssueWithChangelog> GetIssueDetailsAsync(Guid integrationId, string issueKey);
        
        // Task<IList<IssueType>> GetIssueTypesAsync(Guid integrationId); // Keeping it simple for now
        // Task<IList<IssuePriority>> GetPrioritiesAsync(Guid integrationId);
        // Task<IList<IssueStatus>> GetStatusesAsync(Guid integrationId);
    }
}
