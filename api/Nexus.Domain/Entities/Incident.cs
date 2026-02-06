using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Incident : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        
        public required string Title { get; set; }
        public string? ExternalId { get; set; }
        public string? Severity { get; set; } // sev1, sev2
        
        public DateTime StartTime { get; set; }
        public DateTime? DetectedAt { get; set; }
        public DateTime? AcknowledgedAt { get; set; }
        public DateTime? EndTime { get; set; }
        
        public Guid? RootCauseCommitId { get; set; }
        public Commit? RootCauseCommit { get; set; }
        
        public required string Status { get; set; } // open, investigating, resolved
    }
}
