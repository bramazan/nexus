using System;

namespace Nexus.Domain.Entities
{
    public class IssueAssignee
    {
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public DateTime AssignedAt { get; set; }
    }
}
