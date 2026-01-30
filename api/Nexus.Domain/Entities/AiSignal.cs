using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class AiSignal : BaseEntity
    {
        public Guid PullRequestId { get; set; }
        public PullRequest PullRequest { get; set; } = null!;
        
        public required string AiType { get; set; } // agentic, assisted
        public string? ToolName { get; set; }
        public DateTime DetectedAt { get; set; }
    }
}
