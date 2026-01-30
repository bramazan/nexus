using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Team : BaseEntity
    {
        public required string Name { get; set; }
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        
        public Guid? ParentTeamId { get; set; }
        public Team? ParentTeam { get; set; }
        public ICollection<Team> SubTeams { get; set; } = new List<Team>();
        
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<Service> OwnedServices { get; set; } = new List<Service>();
    }
}
