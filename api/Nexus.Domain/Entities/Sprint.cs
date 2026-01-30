using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Sprint : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; } = null!;
        
        public required string Name { get; set; }
        public required string Status { get; set; } // active, future, closed
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
