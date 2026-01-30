using System;

namespace Nexus.Domain.Entities
{
    public class BranchIssueLink
    {
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
    }
}
