using Nexus.Domain.Common;
using System;

namespace Nexus.Domain.Entities
{
    public class PullRequestReview : BaseEntity
    {
        public Guid PullRequestId { get; set; }
        public PullRequest PullRequest { get; set; } = null!;
        
        public Guid? AuthorToolAccountId { get; set; }
        public ToolAccount? AuthorToolAccount { get; set; }
        
        public string? ExternalId { get; set; }
        public string? Body { get; set; }
        
        public required string State { get; set; } // approved, changes_requested, commented
        public DateTime SubmittedAt { get; set; }
    }
}
