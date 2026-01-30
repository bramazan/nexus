using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Persistence.Configurations
{
    public class IssueSprintLinkConfiguration : IEntityTypeConfiguration<IssueSprintLink>
    {
        public void Configure(EntityTypeBuilder<IssueSprintLink> builder)
        {
            builder.HasKey(t => new { t.IssueId, t.SprintId });

            builder.HasOne(t => t.Issue)
                .WithMany(i => i.SprintLinks)
                .HasForeignKey(t => t.IssueId);

            builder.HasOne(t => t.Sprint)
                .WithMany()
                .HasForeignKey(t => t.SprintId);
        }
    }
}
