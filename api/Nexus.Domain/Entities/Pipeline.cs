using Nexus.Domain.Common;

namespace Nexus.Domain.Entities
{
    public class Pipeline : BaseEntity
    {
        public Guid IntegrationId { get; set; }
        public Guid RepositoryId { get; set; }
        
        public int ExternalId { get; set; } // GitLab Pipeline ID
        public int Iid { get; set; }
        
        public string Status { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Ref { get; set; } = string.Empty;
        public string Sha { get; set; } = string.Empty;
        public string? WebUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        public Integration? Integration { get; set; }
        public Repository? Repository { get; set; }
        
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
