using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncDeploymentsCommand(Guid IntegrationId, int ProjectId) : IRequest<int>;

    public class SyncDeploymentsCommandHandler : IRequestHandler<SyncDeploymentsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _gitLabConnector;
        
        public SyncDeploymentsCommandHandler(IApplicationDbContext context, IGitLabConnector gitLabConnector)
        {
            _context = context;
            _gitLabConnector = gitLabConnector;
        }

        public async Task<int> Handle(SyncDeploymentsCommand request, CancellationToken cancellationToken)
        {
            var deployments = await _gitLabConnector.GetDeploymentsAsync(request.IntegrationId, request.ProjectId);
            int count = 0;

            foreach (var gitLabDeployment in deployments)
            {
                var rawEvent = new RawEvent
                {
                    Id = Guid.NewGuid(),
                    IntegrationId = request.IntegrationId,
                    Source = "GitLab",
                    EntityType = "deployment", // Corrected naming
                    EntityId = $"{request.ProjectId}-{gitLabDeployment.Id}",
                    Payload = JsonSerializer.Serialize(gitLabDeployment),
                    OccurredAt = gitLabDeployment.CreatedAt.ToUniversalTime(),
                    IngestedAt = DateTime.UtcNow,
                    Status = ProcessingStatus.Pending
                };

                // Check existence
                var existingEvent = await _context.RawEvents
                    .FirstOrDefaultAsync(r => 
                        r.IntegrationId == request.IntegrationId &&
                        r.EntityType == "deployment" &&
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
