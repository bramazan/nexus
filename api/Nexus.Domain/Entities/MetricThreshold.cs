using Nexus.Domain.Common;

namespace Nexus.Domain.Entities;

public class MetricThreshold : BaseEntity
{
    public string MetricName { get; set; } = string.Empty;
    public string Segment { get; set; } = string.Empty; // e.g., "startup", "enterprise", or "default"
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public string Level { get; set; } = string.Empty; // "Elite", "High", "Medium", "Low"
}
