using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class ServiceMetric : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public DateTime TimeStamp { get; set; }
        
        // DORA & SRE Metrics
        public double? Latency { get; set; } // ms
        public double? ErrorRate { get; set; } // percentage
        public double? Throughput { get; set; } // calls/sec
        
        // Resource Metrics (optional based on raw_data.md Item 3.2 "Application Vitals")
        public double? CpuUsage { get; set; }
        public double? MemoryUsage { get; set; }

        public string? MetricType { get; set; } // "application", "infrastructure"
    }
}
