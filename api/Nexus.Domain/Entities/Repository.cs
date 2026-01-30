using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Repository : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public required string Name { get; set; }
        public string? ExternalId { get; set; }
        public string? Url { get; set; }
        public string? DefaultBranch { get; set; }
        
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
        public ICollection<Commit> Commits { get; set; } = new List<Commit>();
        public ICollection<PullRequest> PullRequests { get; set; } = new List<PullRequest>();
    }
}
