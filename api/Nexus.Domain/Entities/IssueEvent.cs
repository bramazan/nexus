using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class IssueEvent : BaseEntity
    {
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        
        public Guid? ActorToolAccountId { get; set; }
        public ToolAccount? ActorToolAccount { get; set; }
        
        public required string EventType { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }
        
        public DateTime OccurredAt { get; set; }
    }
}
