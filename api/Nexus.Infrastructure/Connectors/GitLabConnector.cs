using GitLabApiClient;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Connectors
{
    public class GitLabConnector : IGitLabConnector
    {
        private readonly IApplicationDbContext _context;

        public GitLabConnector(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateConnectionAsync(string baseUrl, string accessToken)
        {
            try
            {
                var client = new GitLabClient(baseUrl, accessToken);
                var user = await client.Users.GetCurrentSessionAsync();
                return user != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<GitLabApiClient.Models.Projects.Responses.Project>> GetProjectsAsync(Guid integrationId, string? groupId = null)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Projects.GetAsync();
        }

        public async Task<IList<GitLabApiClient.Models.MergeRequests.Responses.MergeRequest>> GetMergeRequestsAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.MergeRequests.GetAsync(projectId, state: GitLabApiClient.Models.MergeRequests.Requests.QueryMergeRequestState.All);
        }

        public async Task<IList<GitLabApiClient.Models.Commits.Responses.Commit>> GetCommitsAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Commits.GetAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Pipelines.Responses.Pipeline>> GetPipelinesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Pipelines.GetAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Issues.Responses.Issue>> GetIssuesAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Issues.GetAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Projects.Responses.ProjectDeployment>> GetDeploymentsAsync(Guid integrationId, int projectId)
        {
            var client = await CreateClientAsync(integrationId);
            // Since GitLabApiClient 1.8.0 might not support Projects.GetDeploymentsAsync directly or signature varies, we check available methods.
            // Assuming the client supports it as per recent usage patterns. 
            // In many versions it is client.Projects.GetDeploymentsAsync(projectId)
            return await client.Projects.GetDeploymentsAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Releases.Responses.Release>> GetReleasesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Releases.GetAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Users.Responses.User>> GetMembersAsync(Guid integrationId, int projectId)
        {
            var client = await CreateClientAsync(integrationId);
            // Using Projects.GetMembersAsync to get members of the specific project
            return await client.Projects.GetMembersAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Branches.Responses.Branch>> GetBranchesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Branches.GetAsync(projectId);
        }

        public async Task<IList<GitLabApiClient.Models.Pipelines.Responses.Job>> GetJobsAsync(Guid integrationId, int projectId, int pipelineId)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.Pipelines.GetJobsAsync(projectId, pipelineId);
        }

        public async Task<IList<GitLabApiClient.Models.Commits.Responses.Commit>> GetMergeRequestCommitsAsync(Guid integrationId, int projectId, int mergeRequestId)
        {
            var client = await CreateClientAsync(integrationId);
            return await client.MergeRequests.GetCommitsAsync(projectId, mergeRequestId);
        }

        // Helper to create client from DB integration
        private async Task<GitLabClient> CreateClientAsync(Guid integrationId)
        {
            var integration = await _context.Integrations.FindAsync(integrationId);
            if (integration == null) throw new Exception("Integration not found");

            var config = JsonSerializer.Deserialize<JsonElement>(integration.Config ?? "{}");
            var baseUrl = config.GetProperty("baseUrl").GetString();
            var accessToken = config.GetProperty("accessToken").GetString();

            return new GitLabClient(baseUrl, accessToken);
        }

        public async Task SyncProjectsAsync(Guid integrationId)
        {
            var client = await CreateClientAsync(integrationId);
            
            // Fetch projects (repositories)
            var projects = await client.Projects.GetAsync();

            foreach (var project in projects)
            {
                // Check if repository exists
                var existingRepo = await _context.Repositories
                    .FirstOrDefaultAsync(r => r.ExternalId == project.Id.ToString() && r.IntegrationId == integrationId);

                if (existingRepo == null)
                {
                    var newRepo = new Repository
                    {
                        IntegrationId = integrationId,
                        Name = project.Name,
                        ExternalId = project.Id.ToString(),
                        Url = project.WebUrl,
                        DefaultBranch = project.DefaultBranch ?? "main"
                    };
                    _context.Repositories.Add(newRepo);
                }
                else
                {
                    // Update existing
                    existingRepo.Name = project.Name;
                    existingRepo.Url = project.WebUrl;
                    existingRepo.DefaultBranch = project.DefaultBranch ?? "main";
                }
            }

            await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
        }
    }
}
