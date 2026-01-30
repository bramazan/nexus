using System;

namespace Nexus.Domain.Entities
{
    public class IssueSprintLink
    {
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        
        public Guid SprintId { get; set; }
        public Sprint Sprint { get; set; } = null!;
        
        public DateTime AddedAt { get; set; }
    }
}
