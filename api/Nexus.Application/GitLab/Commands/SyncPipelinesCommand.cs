using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncPipelinesCommand(Guid IntegrationId, int ProjectId) : IRequest<int>;

    public class SyncPipelinesCommandHandler : IRequestHandler<SyncPipelinesCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _gitLabConnector;

        public SyncPipelinesCommandHandler(IApplicationDbContext context, IGitLabConnector gitLabConnector)
        {
            _context = context;
            _gitLabConnector = gitLabConnector;
        }

        public async Task<int> Handle(SyncPipelinesCommand request, CancellationToken cancellationToken)
        {
            var pipelines = await _gitLabConnector.GetPipelinesAsync(request.IntegrationId, request.ProjectId);
            int count = 0;

            foreach (var gitLabPipeline in pipelines)
            {
                var rawEvent = new RawEvent
                {
                    Id = Guid.NewGuid(),
                    IntegrationId = request.IntegrationId,
                    Source = "GitLab",
                    EntityType = "pipeline", // Corrected naming
                    EntityId = $"{request.ProjectId}-{gitLabPipeline.Id}",
                    Payload = JsonSerializer.Serialize(gitLabPipeline),
                    OccurredAt = gitLabPipeline.CreatedAt.ToUniversalTime(),
                    IngestedAt = DateTime.UtcNow,
                    Status = ProcessingStatus.Pending
                };

                // Check existence
                var existingEvent = await _context.RawEvents
                    .FirstOrDefaultAsync(r => 
                        r.IntegrationId == request.IntegrationId &&
                        r.EntityType == "pipeline" &&
                        r.EntityId == rawEvent.EntityId,
                        cancellationToken);

                if (existingEvent != null)
                {
                    existingEvent.Payload = rawEvent.Payload;
                    existingEvent.IngestedAt = DateTime.UtcNow;
                    existingEvent.Status = ProcessingStatus.Pending;
                    existingEvent.ProcessedAt = null;
                    existingEvent.ErrorMessage = null;
                }
                else
                {
                    _context.RawEvents.Add(rawEvent);
                }
                count++;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return count;
        }
    }
}
