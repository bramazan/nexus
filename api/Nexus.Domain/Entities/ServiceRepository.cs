using System;

namespace Nexus.Domain.Entities
{
    public class ServiceRepository
    {
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;
        
        public string? PathFilter { get; set; }
    }
}
