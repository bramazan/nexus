using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class CodeChange : BaseEntity
    {
        public Guid CommitId { get; set; }
        public Commit Commit { get; set; } = null!;
        
        public required string FilePath { get; set; }
        public int LinesAdded { get; set; }
        public int LinesDeleted { get; set; }
        public bool IsRefactor { get; set; }
        public bool IsRework { get; set; }
    }
}
