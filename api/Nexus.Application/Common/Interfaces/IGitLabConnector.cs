using System.Threading.Tasks;

namespace Nexus.Application.Common.Interfaces
{
    public interface IGitLabConnector
    {
        Task<bool> ValidateConnectionAsync(string baseUrl, string accessToken);
        
        // Core Resources
        Task<IList<GitLabApiClient.Models.Projects.Responses.Project>> GetProjectsAsync(Guid integrationId, string? groupId = null);
        Task<IList<GitLabApiClient.Models.MergeRequests.Responses.MergeRequest>> GetMergeRequestsAsync(Guid integrationId, int projectId, DateTime? since = null);
        Task<IList<GitLabApiClient.Models.Commits.Responses.Commit>> GetCommitsAsync(Guid integrationId, int projectId, DateTime? since = null);
        Task<IList<GitLabApiClient.Models.Pipelines.Responses.Pipeline>> GetPipelinesAsync(Guid integrationId, int projectId);
        Task<IList<GitLabApiClient.Models.Issues.Responses.Issue>> GetIssuesAsync(Guid integrationId, int projectId, DateTime? since = null);
        
        // Extended Resources for DORA & Team Structure
        Task<IList<GitLabApiClient.Models.Projects.Responses.ProjectDeployment>> GetDeploymentsAsync(Guid integrationId, int projectId);
        Task<IList<GitLabApiClient.Models.Releases.Responses.Release>> GetReleasesAsync(Guid integrationId, int projectId);
        Task<IList<GitLabApiClient.Models.Users.Responses.User>> GetMembersAsync(Guid integrationId, int projectId);
        Task<IList<GitLabApiClient.Models.Branches.Responses.Branch>> GetBranchesAsync(Guid integrationId, int projectId);
        
        // Deep Dive
        Task<IList<GitLabApiClient.Models.Pipelines.Responses.Job>> GetJobsAsync(Guid integrationId, int projectId, int pipelineId);
        Task<IList<GitLabApiClient.Models.Commits.Responses.Commit>> GetMergeRequestCommitsAsync(Guid integrationId, int projectId, int mergeRequestId);
        
        // Legacy Sync
        Task SyncProjectsAsync(Guid integrationId);
    }
}
