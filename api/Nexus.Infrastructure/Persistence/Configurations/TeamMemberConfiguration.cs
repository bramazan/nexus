using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Persistence.Configurations
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(t => new { t.TeamId, t.UserId });

            builder.HasOne(t => t.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(t => t.TeamId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
}
