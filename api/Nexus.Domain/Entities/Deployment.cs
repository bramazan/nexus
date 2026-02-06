using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Deployment : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        
        public required string Environment { get; set; } // prod, staging
        
        public string? ExternalId { get; set; } // GitLab Deployment ID (or specific ID)
        
        public DateTime StartedAt { get; set; }
        public DateTime? DeployedAt { get; set; }
        
        public required string Status { get; set; } // success, failed, cancelled
        
        public Guid? CommitId { get; set; }
        public Commit? Commit { get; set; }
        
        public Guid? TriggerActorId { get; set; }
        public User? TriggerActor { get; set; }
    }
}
