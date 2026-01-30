using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Release : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        
        public required string Version { get; set; }
        public DateTime ReleasedAt { get; set; }
        public string? Description { get; set; }
    }
}
