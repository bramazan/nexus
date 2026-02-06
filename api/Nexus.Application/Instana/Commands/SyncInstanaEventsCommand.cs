using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;

namespace Nexus.Application.Instana.Commands
{
    public record SyncInstanaEventsCommand(Guid IntegrationId) : IRequest<int>;

    public class SyncInstanaEventsCommandHandler : IRequestHandler<SyncInstanaEventsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInstanaConnector _instanaConnector;

        public SyncInstanaEventsCommandHandler(IApplicationDbContext context, IInstanaConnector instanaConnector)
        {
            _context = context;
            _instanaConnector = instanaConnector;
        }

        public async Task<int> Handle(SyncInstanaEventsCommand request, CancellationToken cancellationToken)
        {
            // Fetch events for last 24 hours
            long to = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            long from = new DateTimeOffset(DateTime.UtcNow.AddDays(-1)).ToUnixTimeMilliseconds();

            var events = await _instanaConnector.GetEventsAsync(request.IntegrationId, from, to);
            int count = 0;

            foreach (var evt in events)
            {
                // Only process "issue" or "incident" types
                if (evt.Type == "issue" || evt.Type == "incident")
                {
                    var incident = await _context.Incidents
                        .FirstOrDefaultAsync(i => i.ExternalId == evt.EventId, cancellationToken);
                    
                    // Attempt to link to Service (EntityId often contains service name or ID)
                    // Simplified linking logic: Search Service by Name match in Text or EntityId
                    // Note: Filtering by IntegrationId on Service is not possible directly as Service doesn't have it.
                    // We rely on name matching within the whole DB or we need to look up via Workspace if Integration is linked to Workspace.
                    var service = await _context.Services
                        .FirstOrDefaultAsync(s => evt.EntityId.Contains(s.Name), cancellationToken);

                    if (service == null) continue; // Skip if cannot link to known service

                    if (incident == null)
                    {
                        incident = new Incident
                        {
                            ServiceId = service.Id,
                            ExternalId = evt.EventId,
                            Title = evt.Text,
                            Severity = evt.Severity > 5 ? "sev1" : "sev2", // Threshold simplified
                            StartTime = DateTime.UnixEpoch.AddMilliseconds(evt.Start).ToUniversalTime(),
                            EndTime = evt.End.HasValue ? DateTime.UnixEpoch.AddMilliseconds(evt.End.Value).ToUniversalTime() : null,
                            Status = evt.End.HasValue ? "resolved" : "open"
                        };
                        _context.Incidents.Add(incident);
                    }
                    else
                    {
                        incident.EndTime = evt.End.HasValue ? DateTime.UnixEpoch.AddMilliseconds(evt.End.Value).ToUniversalTime() : null;
                        incident.Status = evt.End.HasValue ? "resolved" : "open";
                    }
                    count++;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return count;
        }
    }
}
