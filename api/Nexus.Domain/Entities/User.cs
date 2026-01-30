using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }
    }
}
