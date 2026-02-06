using MediatR;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common.Interfaces;
using Nexus.Domain.Entities;

namespace Nexus.Application.Instana.Commands
{
    public record SyncInstanaMetricsCommand(Guid IntegrationId) : IRequest<int>;

    public class SyncInstanaMetricsCommandHandler : IRequestHandler<SyncInstanaMetricsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInstanaConnector _instanaConnector;

        public SyncInstanaMetricsCommandHandler(IApplicationDbContext context, IInstanaConnector instanaConnector)
        {
            _context = context;
            _instanaConnector = instanaConnector;
        }

        public async Task<int> Handle(SyncInstanaMetricsCommand request, CancellationToken cancellationToken)
        {
            // 1. Get Instana Applications (to get context)
            var apps = await _instanaConnector.GetApplicationsAsync(request.IntegrationId);
            int count = 0;

            foreach (var app in apps)
            {
                // 2. Get Services for Application
                var instanaServices = await _instanaConnector.GetServicesAsync(request.IntegrationId, app.Id);

                foreach (var iService in instanaServices)
                {
                     // 3. Match with Nexus Service by Name
                     var nexusService = await _context.Services
                        .FirstOrDefaultAsync(s => s.Name == iService.Label, cancellationToken);
                     
                     if (nexusService != null)
                     {
                        // 4. Fetch metrics
                         var response = await _instanaConnector.GetMetricsAsync(request.IntegrationId, $"entity.serviceId:{iService.Id}", 300000);
                
                        if (response.Items.Any())
                        {
                            var metric = new ServiceMetric
                            {
                                ServiceId = nexusService.Id,
                                TimeStamp = DateTime.UtcNow,
                                MetricType = "application",
                                // Simplified mapping
                            };
                            _context.ServiceMetrics.Add(metric);
                            count++;
                        }
                     }
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return count;
        }
    }
}
