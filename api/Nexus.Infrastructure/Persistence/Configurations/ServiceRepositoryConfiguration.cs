using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Persistence.Configurations
{
    public class ServiceRepositoryConfiguration : IEntityTypeConfiguration<ServiceRepository>
    {
        public void Configure(EntityTypeBuilder<ServiceRepository> builder)
        {
            builder.HasKey(t => new { t.ServiceId, t.RepositoryId });

            builder.HasOne(t => t.Service)
                .WithMany(s => s.ServiceRepositories)
                .HasForeignKey(t => t.ServiceId);

            builder.HasOne(t => t.Repository)
                .WithMany()
                .HasForeignKey(t => t.RepositoryId);
        }
    }
}
