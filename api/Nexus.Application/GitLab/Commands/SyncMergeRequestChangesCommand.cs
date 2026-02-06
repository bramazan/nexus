using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncMergeRequestChangesCommand(Guid IntegrationId, string ProjectId, int MergeRequestIid) : IRequest<bool>;

    public class SyncMergeRequestChangesCommandHandler : IRequestHandler<SyncMergeRequestChangesCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _connector;

        public SyncMergeRequestChangesCommandHandler(IApplicationDbContext context, IGitLabConnector connector)
        {
            _context = context;
            _connector = connector;
        }

        public async Task<bool> Handle(SyncMergeRequestChangesCommand request, CancellationToken cancellationToken)
        {
            var endpoint = $"api/v4/projects/{Uri.EscapeDataString(request.ProjectId)}/merge_requests/{request.MergeRequestIid}/changes";
            
            try 
            {
                var jsonElement = await _connector.GetRawDataAsync(request.IntegrationId, endpoint);
                
                if (jsonElement.ValueKind == JsonValueKind.Undefined || jsonElement.ValueKind == JsonValueKind.Null)
                {
                    return false;
                }

                // We store the whole changes payload as one raw event linked to the MR
                // EntityId strategy: "{ProjectId}-{Iid}-changes"
                string entityId = $"{request.ProjectId}-{request.MergeRequestIid}-changes";
                
                // Check redundancy?
                // For changes, they might be updated if the MR is updated. 
                // We should probably allow re-ingestion if updated_at changed?
                // For simplicity, let's just insert/update raw event.
                
                var existingEvent = await _context.RawEvents
                    .FirstOrDefaultAsync(r => r.IntegrationId == request.IntegrationId 
                                           && r.EntityType == "code_change" 
                                           && r.EntityId == entityId, cancellationToken);

                if (existingEvent != null)
                {
                    // Update existing
                    existingEvent.Payload = jsonElement.GetRawText();
                    existingEvent.OccurredAt = DateTime.UtcNow; // Or try to find updated_at in payload
                    existingEvent.IngestedAt = DateTime.UtcNow;
                    existingEvent.Status = ProcessingStatus.Pending; // Reprocess
                }
                else
                {
                    var rawEvent = new RawEvent
                    {
                        IntegrationId = request.IntegrationId,
                        Source = "GitLab",
                        EntityType = "code_change",
                        EntityId = entityId,
                        Payload = jsonElement.GetRawText(),
                        OccurredAt = DateTime.UtcNow,
                        IngestedAt = DateTime.UtcNow,
                        Status = ProcessingStatus.Pending
                    };
                    _context.RawEvents.Add(rawEvent);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
