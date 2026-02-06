using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;
using System.Text.Json;

namespace Nexus.Application.GitLab.Commands
{
    public record SyncReleasesCommand(Guid IntegrationId, int ProjectId) : IRequest<int>;

    public class SyncReleasesCommandHandler : IRequestHandler<SyncReleasesCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGitLabConnector _gitLabConnector;

        public SyncReleasesCommandHandler(IApplicationDbContext context, IGitLabConnector gitLabConnector)
        {
            _context = context;
            _gitLabConnector = gitLabConnector;
        }

        public async Task<int> Handle(SyncReleasesCommand request, CancellationToken cancellationToken)
        {
            var releases = await _gitLabConnector.GetReleasesAsync(request.IntegrationId, request.ProjectId);
            int count = 0;

            foreach (var gitLabRelease in releases)
            {
                var rawEvent = new RawEvent
                {
                    Id = Guid.NewGuid(),
                    IntegrationId = request.IntegrationId,
                    Source = "GitLab",
                    EntityType = "release", // As per raw_data.md
                    EntityId = $"{request.ProjectId}-{gitLabRelease.TagName}",
                    Payload = JsonSerializer.Serialize(gitLabRelease),
                    OccurredAt = (gitLabRelease.ReleasedAt != default ? gitLabRelease.ReleasedAt : gitLabRelease.CreatedAt).ToUniversalTime(),
                    IngestedAt = DateTime.UtcNow,
                    Status = ProcessingStatus.Pending
                };

                // Check existence
                var existingEvent = await _context.RawEvents
                    .FirstOrDefaultAsync(r => 
                        r.IntegrationId == request.IntegrationId &&
                        r.EntityType == "release" &&
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
