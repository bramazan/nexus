using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Application.Common.Models;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Nexus.Infrastructure.Connectors
{
    public class GitLabConnector : IGitLabConnector
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public GitLabConnector(IApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ValidateConnectionAsync(string baseUrl, string accessToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("PRIVATE-TOKEN", accessToken);
                
                var response = await client.GetAsync("api/v4/user");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<GitLabProject>> GetProjectsAsync(Guid integrationId, string? groupId = null)
        {
            var client = await CreateHttpClientAsync(integrationId);
            string url;

            if (!string.IsNullOrEmpty(groupId))
            {
                url = $"api/v4/groups/{groupId}/projects?include_subgroups=true&per_page=100";
            }
            else
            {
                url = "api/v4/projects?membership=true&simple=true&per_page=100";
            }
            
            return await client.GetFromJsonAsync<IList<GitLabProject>>(url) ?? new List<GitLabProject>();
        }

        public async Task<IList<GitLabMergeRequest>> GetMergeRequestsAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/merge_requests?per_page=100";
            if (since.HasValue)
            {
                url += $"&created_after={since.Value:O}";
            }
            
            return await client.GetFromJsonAsync<IList<GitLabMergeRequest>>(url) ?? new List<GitLabMergeRequest>();
        }

        public async Task<IList<GitLabCommit>> GetCommitsAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/repository/commits?per_page=100";
            if (since.HasValue)
            {
                url += $"&since={since.Value:O}";
            }
            
            return await client.GetFromJsonAsync<IList<GitLabCommit>>(url) ?? new List<GitLabCommit>();
        }

        public async Task<IList<GitLabPipeline>> GetPipelinesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/pipelines?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabPipeline>>(url) ?? new List<GitLabPipeline>();
        }

        public async Task<IList<GitLabIssue>> GetIssuesAsync(Guid integrationId, int projectId, DateTime? since = null)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/issues?per_page=100";
            if (since.HasValue)
            {
                url += $"&created_after={since.Value:O}";
            }
            
            return await client.GetFromJsonAsync<IList<GitLabIssue>>(url) ?? new List<GitLabIssue>();
        }

        public async Task<IList<GitLabDeployment>> GetDeploymentsAsync(Guid integrationId, int projectId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/deployments?per_page=100&order_by=created_at&sort=desc";
            
            return await client.GetFromJsonAsync<IList<GitLabDeployment>>(url) ?? new List<GitLabDeployment>();
        }

        public async Task<IList<GitLabRelease>> GetReleasesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/releases?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabRelease>>(url) ?? new List<GitLabRelease>();
        }

        public async Task<IList<GitLabMember>> GetMembersAsync(Guid integrationId, int projectId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/members/all?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabMember>>(url) ?? new List<GitLabMember>();
        }

        public async Task<IList<GitLabBranch>> GetBranchesAsync(Guid integrationId, int projectId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/repository/branches?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabBranch>>(url) ?? new List<GitLabBranch>();
        }

        public async Task<IList<GitLabJob>> GetJobsAsync(Guid integrationId, int projectId, int pipelineId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/pipelines/{pipelineId}/jobs?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabJob>>(url) ?? new List<GitLabJob>();
        }

        public async Task<IList<GitLabCommit>> GetMergeRequestCommitsAsync(Guid integrationId, int projectId, int mergeRequestId)
        {
            var client = await CreateHttpClientAsync(integrationId);
            var url = $"api/v4/projects/{projectId}/merge_requests/{mergeRequestId}/commits?per_page=100";
            
            return await client.GetFromJsonAsync<IList<GitLabCommit>>(url) ?? new List<GitLabCommit>();
        }

        // Helper to create client from DB integration
        private async Task<HttpClient> CreateHttpClientAsync(Guid integrationId)
        {
            var integration = await _context.Integrations.FindAsync(integrationId);
            if (integration == null) throw new Exception("Integration not found");

            var config = JsonSerializer.Deserialize<JsonElement>(integration.Config ?? "{}");
            var baseUrl = config.GetProperty("baseUrl").GetString();
            var accessToken = config.GetProperty("accessToken").GetString();

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("PRIVATE-TOKEN", accessToken);
            
            return client;
        }

        public async Task SyncProjectsAsync(Guid integrationId)
        {
            var projects = await GetProjectsAsync(integrationId);

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
                        DefaultBranch = project.DefaultBranch
                    };
                    _context.Repositories.Add(newRepo);
                }
                else
                {
                    // Update existing
                    existingRepo.Name = project.Name;
                    existingRepo.Url = project.WebUrl;
                    existingRepo.DefaultBranch = project.DefaultBranch;
                }
            }

            await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
        }
    }
}
