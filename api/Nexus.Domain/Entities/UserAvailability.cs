using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class UserAvailability : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public required string Type { get; set; } // vacation, sick
        public string? Reason { get; set; }
    }
}
