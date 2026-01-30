using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Integration : BaseEntity
    {
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        
        public required string Type { get; set; } // github, jira, linear
        public required string Name { get; set; }
        public string? Config { get; set; } // JSON stored as string
    }
}
