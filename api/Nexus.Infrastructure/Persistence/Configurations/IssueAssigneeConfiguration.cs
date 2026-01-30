using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Persistence.Configurations
{
    public class IssueAssigneeConfiguration : IEntityTypeConfiguration<IssueAssignee>
    {
        public void Configure(EntityTypeBuilder<IssueAssignee> builder)
        {
            builder.HasKey(t => new { t.IssueId, t.UserId });

            builder.HasOne(t => t.Issue)
                .WithMany(i => i.Assignees)
                .HasForeignKey(t => t.IssueId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
}
