using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nexus.Application.Common.Models;

namespace Nexus.Application.Common.Interfaces
{
    public interface IInstanaConnector
    {
        Task<IList<InstanaApplication>> GetApplicationsAsync(Guid integrationId);
        Task<IList<InstanaService>> GetServicesAsync(Guid integrationId, string applicationId);
        
        // Metrics: Calls /api/infrastructure-monitoring/metrics or /api/application-monitoring/metrics
        Task<InstanaMetricResponse> GetMetricsAsync(Guid integrationId, string query, long windowSize);
        
        // Events: Useful for MTTR
        Task<IList<InstanaEvent>> GetEventsAsync(Guid integrationId, long from, long to);
    }
}
