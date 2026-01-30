using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class RawEvent : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public required string Source { get; set; }
        public required string EntityType { get; set; }
        public required string EntityId { get; set; }
        
        public required string Payload { get; set; } // JSON
        
        public DateTime OccurredAt { get; set; }
        public DateTime IngestedAt { get; set; }
    }
}
