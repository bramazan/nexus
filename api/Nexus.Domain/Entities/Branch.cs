using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;
        
        public required string Name { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
