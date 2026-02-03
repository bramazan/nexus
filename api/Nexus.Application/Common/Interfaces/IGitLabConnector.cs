using System.Threading.Tasks;
using System.Text.Json;
using Nexus.Application.Common.Models;

namespace Nexus.Application.Common.Interfaces
{
    public interface IGitLabConnector
    {
        Task<bool> ValidateConnectionAsync(string baseUrl, string accessToken);
        
        // Core Resources
        Task<IList<GitLabProject>> GetProjectsAsync(Guid integrationId, string? groupId = null);
        Task<IList<GitLabMergeRequest>> GetMergeRequestsAsync(Guid integrationId, int projectId, DateTime? since = null);
        Task<IList<GitLabCommit>> GetCommitsAsync(Guid integrationId, int projectId, DateTime? since = null);
        Task<IList<GitLabPipeline>> GetPipelinesAsync(Guid integrationId, int projectId);
        Task<IList<GitLabIssue>> GetIssuesAsync(Guid integrationId, int projectId, DateTime? since = null);
        
        // Extended Resources for DORA & Team Structure
        Task<IList<GitLabDeployment>> GetDeploymentsAsync(Guid integrationId, int projectId);
        Task<IList<GitLabRelease>> GetReleasesAsync(Guid integrationId, int projectId);
        Task<IList<GitLabMember>> GetMembersAsync(Guid integrationId, int projectId);
        Task<IList<GitLabBranch>> GetBranchesAsync(Guid integrationId, int projectId);
        
        // Deep Dive
        Task<IList<GitLabJob>> GetJobsAsync(Guid integrationId, int projectId, int pipelineId);
        Task<IList<GitLabCommit>> GetMergeRequestCommitsAsync(Guid integrationId, int projectId, int mergeRequestId);
        
        // Legacy Sync
        Task SyncProjectsAsync(Guid integrationId);

        // Raw Data Access
        Task<JsonElement> GetRawDataAsync(Guid integrationId, string endpoint);
    }
}
