using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class IssueEstimate : BaseEntity
    {
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        
        public double EstimateValue { get; set; }
        public required string EstimateUnit { get; set; } // points, hours
    }
}
