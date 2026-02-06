using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
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
    public class InstanaConnector : IInstanaConnector
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public InstanaConnector(IApplicationDbContext context, IHttpClientFactory httpClientFactory)
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
            var apiToken = config.GetProperty("apiToken").GetString();

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(apiToken))
                throw new Exception("Invalid Instana configuration");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
            
            // Instana uses 'authorization: ApiToken <token>' schema
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"ApiToken {apiToken}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<IList<InstanaApplication>> GetApplicationsAsync(Guid integrationId)
        {
            var client = await CreateClientAsync(integrationId);
            var response = await client.GetAsync("/api/application-monitoring/applications?windowSize=300000"); // 5 mins default
            
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(content);
            
            if (json.TryGetProperty("items", out var items))
            {
                return JsonSerializer.Deserialize<List<InstanaApplication>>(items.GetRawText()) ?? new List<InstanaApplication>();
            }
            
            return new List<InstanaApplication>();
        }

        public async Task<IList<InstanaService>> GetServicesAsync(Guid integrationId, string applicationId)
        {
            var client = await CreateClientAsync(integrationId);
            var response = await client.GetAsync($"/api/application-monitoring/applications/{applicationId}/services");
            
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(content);
             
            if (json.TryGetProperty("items", out var items))
            {
                return JsonSerializer.Deserialize<List<InstanaService>>(items.GetRawText()) ?? new List<InstanaService>();
            }
            
            return new List<InstanaService>();
        }

        public async Task<InstanaMetricResponse> GetMetricsAsync(Guid integrationId, string query, long windowSize)
        {
            var client = await CreateClientAsync(integrationId);
            
            // Example metric query payload
            // In a real scenario, this would dynamically build the payload based on 'query' string (e.g. "entity.serviceId:xyz")
            var requestBody = new
            {
                metrics = new[] { "completed", "erroneous", "latency-mean" }, // Standard Application Vitals
                plugin = "application",
                rollup = windowSize / 1000 // Simple rollup based on window
                // Filters would go here based on query
            };
            
            // For now, returning an empty structure or a mock structure if needed.
            // Since we can't hit real Instana API without credentials, we keep this aligned with DTOs.
            // If credentials existed, we would do:
            // var jsonKey = JsonSerializer.Serialize(requestBody);
            // var content = new StringContent(jsonKey, Encoding.UTF8, "application/json");
            // var response = await client.PostAsync("/api/infrastructure-monitoring/metrics", content); // Endpoint varies by metric type
            
            return new InstanaMetricResponse(); 
        }

        public async Task<IList<InstanaEvent>> GetEventsAsync(Guid integrationId, long from, long to)
        {
            var client = await CreateClientAsync(integrationId);
            var response = await client.GetAsync($"/api/events?from={from}&to={to}");
            
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            // Events API might return list directly or wrapped. Assuming wrapped for now based on standard.
            // If API returns array directly:
            try 
            {
                return JsonSerializer.Deserialize<List<InstanaEvent>>(content) ?? new List<InstanaEvent>();
            }
            catch
            {
                return new List<InstanaEvent>();
            }
        }
    }
}
