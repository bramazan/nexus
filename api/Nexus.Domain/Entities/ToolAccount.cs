using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class ToolAccount : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public string? ExternalId { get; set; }
        public required string Username { get; set; }
        
        public string? ExternalEmail { get; set; }
        public string? DisplayName { get; set; }
        public string? ExternalMetadata { get; set; }
        
        public bool IsActive { get; set; }
    }
}
