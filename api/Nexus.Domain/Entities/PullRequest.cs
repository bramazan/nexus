using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class PullRequest : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;
        
        public string? ExternalId { get; set; }
        public int Number { get; set; }
        public required string Title { get; set; }
        
        public Guid? AuthorToolAccountId { get; set; }
        public ToolAccount? AuthorToolAccount { get; set; }
        
        public required string State { get; set; } // open, closed, merged
        
        public string? SourceBranch { get; set; }
        public string? TargetBranch { get; set; }
        public string? Description { get; set; }
        
        public DateTime? FirstReviewAt { get; set; }
        public DateTime? MergedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        
        public int SizeLines { get; set; }
        public bool IsAiGenerated { get; set; }
        
        public int ReviewCount { get; set; }
        public int? CycleTimeMinutes { get; set; }
        
        public ICollection<PullRequestReview> Reviews { get; set; } = new List<PullRequestReview>();
        public ICollection<AiSignal> AiSignals { get; set; } = new List<AiSignal>();
    }
}
