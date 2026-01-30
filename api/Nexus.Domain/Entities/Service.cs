using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Service : BaseEntity
    {
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        
        public required string Name { get; set; }
        public string? Description { get; set; }
        
        public Guid? OwnerTeamId { get; set; }
        public Team? OwnerTeam { get; set; }
        
        public string? Tier { get; set; }
        public string? Lifecycle { get; set; }
        
        public ICollection<ServiceRepository> ServiceRepositories { get; set; } = new List<ServiceRepository>();
        public ICollection<Deployment> Deployments { get; set; } = new List<Deployment>();
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
