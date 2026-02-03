using MediatR;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncGitLabRawDataCommand(Guid IntegrationId, string ProjectId, string DataType) : IRequest<int>;

    public class SyncGitLabRawDataCommandHandler : IRequestHandler<SyncGitLabRawDataCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _connector;

        public SyncGitLabRawDataCommandHandler(IApplicationDbContext context, IGitLabConnector connector)
        {
            _context = context;
            _connector = connector;
        }

        public async Task<int> Handle(SyncGitLabRawDataCommand request, CancellationToken cancellationToken)
        {
            string baseEndpoint = request.DataType.ToLower() switch
            {
                "commits" or "commit" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/repository/commits?per_page=100",
                "mergerequests" or "mrs" or "pull_request" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/merge_requests?per_page=100",
                "pipelines" or "deployment" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/pipelines?per_page=100",
                "jobs" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs?per_page=100",
                "issues" or "issue" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/issues?per_page=100",
                "deployments" or "deployment_event" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/deployments?per_page=100",
                "releases" or "release" => $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/releases?per_page=100",
                _ => throw new ArgumentException($"Unsupported DataType: {request.DataType}")
            };

            int page = 1;
            int totalSynced = 0;
            bool hasMore = true;

            while (hasMore)
            {
                var endpoint = $"{baseEndpoint}&page={page}";
                var jsonElement = await _connector.GetRawDataAsync(request.IntegrationId, endpoint);

                if (jsonElement.ValueKind != JsonValueKind.Array || jsonElement.GetArrayLength() == 0)
                {
                    hasMore = false;
                    break;
                }

                var items = jsonElement.EnumerateArray().ToList();
                var newRawEvents = new List<RawEvent>();
                var itemIds = new List<string>();

                // First pass: extract IDs and prepare list
                foreach (var item in items)
                {
                    string entityId = ExtractEntityId(item);
                    itemIds.Add(entityId);
                }

                // Check which IDs already exist in DB for this Integration + DataType
                // Note: We check against the database to avoid duplicates. 
                // For very large batches, we might need to be careful with "Contains" clause size, 
                // but 100 items per page is safe for SQL IN clause.
                var existingEntityIds = await _context.RawEvents
                    .Where(r => r.IntegrationId == request.IntegrationId 
                             && r.EntityType == request.DataType 
                             && itemIds.Contains(r.EntityId))
                    .Select(r => r.EntityId)
                    .ToListAsync(cancellationToken);

                var existingIdsSet = new HashSet<string>(existingEntityIds);

                foreach (var item in items)
                {
                    string entityId = ExtractEntityId(item);

                    if (!existingIdsSet.Contains(entityId))
                    {
                        var rawEvent = new RawEvent
                        {
                            IntegrationId = request.IntegrationId,
                            Source = "GitLab",
                            EntityType = request.DataType,
                            EntityId = entityId,
                            Payload = item.GetRawText(),
                            OccurredAt = DateTime.UtcNow,
                            IngestedAt = DateTime.UtcNow
                        };
                        newRawEvents.Add(rawEvent);
                    }
                }

                if (newRawEvents.Count > 0)
                {
                    _context.RawEvents.AddRange(newRawEvents);
                    await _context.SaveChangesAsync(cancellationToken);
                    totalSynced += newRawEvents.Count;
                }

                // If we received fewer items than the page size (100), we know we are done.
                // However, checking if items.Count < 100 is a good optimization, 
                // but continuing to the next page to receive an empty list is safer for edge cases (exactly 100 items).
                if (items.Count < 100)
                {
                    hasMore = false;
                }
                else
                {
                    page++;
                }
            }

            return totalSynced;
        }
        
        private string ExtractEntityId(JsonElement item)
        {
             if (item.TryGetProperty("id", out var idProp)) return idProp.ToString();
             if (item.TryGetProperty("iid", out var iidProp)) return iidProp.ToString();
             if (item.TryGetProperty("sha", out var shaProp)) return shaProp.ToString();
             return Guid.NewGuid().ToString();
        }
    }
}
