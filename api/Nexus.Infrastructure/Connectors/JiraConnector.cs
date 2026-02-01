using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Connectors
{
    public class JiraConnector : IJiraConnector
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public JiraConnector(IApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<HttpClient> CreateClientAsync(Guid integrationId)
        {
            var integration = await _context.Integrations.FindAsync(integrationId);
            if (integration == null) throw new Exception("Integration not found");

            var config = JsonSerializer.Deserialize<JsonElement>(integration.Config ?? "{}");
            
            var baseUrl = config.GetProperty("baseUrl").GetString()?.TrimEnd('/');
            var email = config.GetProperty("email").GetString();
            var apiToken = config.GetProperty("apiToken").GetString();

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(apiToken))
                throw new Exception("Invalid Jira configuration");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
            
            var authBytes = Encoding.ASCII.GetBytes($"{email}:{apiToken}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<IList<JiraProject>> GetProjectsAsync(Guid integrationId)
        {
            var client = await CreateClientAsync(integrationId);
            var response = await client.GetAsync("/rest/api/3/project");
            
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var projects = JsonSerializer.Deserialize<List<JiraProject>>(content) ?? new List<JiraProject>();
            
            return projects;
        }

        public async Task<JiraIssueSearchResponse> GetIssuesAsync(Guid integrationId, string jql, int startAt = 0, int maxResults = 50)
        {
            var client = await CreateClientAsync(integrationId);
            
            // Using POST search for better JQL support
            var requestBody = new
            {
                jql = jql,
                startAt = startAt,
                maxResults = maxResults,
                fields = new[] { "summary", "status", "priority", "issuetype", "created", "updated" } 
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync("/rest/api/3/search", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<JiraIssueSearchResponse>(responseContent) ?? new JiraIssueSearchResponse();
            
            return searchResult;
        }

        public async Task<IList<JiraBoard>> GetBoardsAsync(Guid integrationId)
        {
            var client = await CreateClientAsync(integrationId);
            // Agile API usually under /rest/agile/1.0
            var response = await client.GetAsync("/rest/agile/1.0/board");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var boardResponse = JsonSerializer.Deserialize<JiraBoardResponse>(content);
            
            return boardResponse?.Values ?? new List<JiraBoard>();
        }

        public async Task<IList<JiraSprint>> GetSprintsAsync(Guid integrationId, int boardId)
        {
            var client = await CreateClientAsync(integrationId);
            var response = await client.GetAsync($"/rest/agile/1.0/board/{boardId}/sprint");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var sprintResponse = JsonSerializer.Deserialize<JiraSprintResponse>(content);
            
            return sprintResponse?.Values ?? new List<JiraSprint>();
        }

        public async Task<JiraIssueWithChangelog> GetIssueDetailsAsync(Guid integrationId, string issueKey)
        {
            var client = await CreateClientAsync(integrationId);
            // Fetch issue with changelog expansion
            var response = await client.GetAsync($"/rest/api/3/issue/{issueKey}?expand=changelog");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var issue = JsonSerializer.Deserialize<JiraIssueWithChangelog>(content);
            
            if (issue == null) throw new Exception("Issue not found");
            
            return issue;
        }
    }
}
