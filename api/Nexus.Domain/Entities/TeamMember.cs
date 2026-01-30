using System;

namespace Nexus.Domain.Entities
{
    // Composite Key Entity
    public class TeamMember
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string? Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
