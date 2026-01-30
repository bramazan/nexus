using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public string? ExternalId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        
        public required string Status { get; set; } // todo, in_progress, done
        public required string Type { get; set; } // bug, story, task
        public string? Priority { get; set; }
        
        public Guid? ParentIssueId { get; set; }
        public Issue? ParentIssue { get; set; }
        
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public ICollection<IssueEvent> Events { get; set; } = new List<IssueEvent>();
        public ICollection<IssueEstimate> Estimates { get; set; } = new List<IssueEstimate>();
        public ICollection<IssueAssignee> Assignees { get; set; } = new List<IssueAssignee>();
        public ICollection<IssueSprintLink> SprintLinks { get; set; } = new List<IssueSprintLink>();
    }
}
