using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Persistence.Configurations
{
    public class BranchIssueLinkConfiguration : IEntityTypeConfiguration<BranchIssueLink>
    {
        public void Configure(EntityTypeBuilder<BranchIssueLink> builder)
        {
            builder.HasKey(t => new { t.BranchId, t.IssueId });

            builder.HasOne(t => t.Branch)
                .WithMany()
                .HasForeignKey(t => t.BranchId);

            builder.HasOne(t => t.Issue)
                .WithMany() // Assuming Issue doesn't have a collection back to BranchIssueLink for now
                .HasForeignKey(t => t.IssueId);
        }
    }
}
