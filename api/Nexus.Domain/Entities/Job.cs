using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Job : BaseEntity
    {
        public Guid PipelineId { get; set; }
        public Pipeline Pipeline { get; set; } = null!;

        public required string Name { get; set; }
        public string? Stage { get; set; }
        public string? Status { get; set; }
        
        public double? Duration { get; set; } // Seconds

        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        
        public string? ExternalId { get; set; }
        public string? WebUrl { get; set; }
    }
}
