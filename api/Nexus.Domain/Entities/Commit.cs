using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Commit : BaseEntity
    {
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;
        
        public required string Sha { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorEmail { get; set; }
        public DateTime AuthoredDate { get; set; }
        public DateTime CommittedAt { get; set; }
        public string? Message { get; set; }
        public string? WebUrl { get; set; }
        
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        
        public ICollection<CodeChange> CodeChanges { get; set; } = new List<CodeChange>();
    }
}
