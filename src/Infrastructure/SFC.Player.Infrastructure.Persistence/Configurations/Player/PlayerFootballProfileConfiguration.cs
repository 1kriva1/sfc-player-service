using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Data;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Player;
public class PlayerFootballProfileConfiguration : IEntityTypeConfiguration<PlayerFootballProfile>
{
    public void Configure(EntityTypeBuilder<PlayerFootballProfile> builder)
    {
        builder.Property(e => e.Height)
               .IsRequired(false);

        builder.Property(e => e.Weight)
               .IsRequired(false);

        builder.Property(e => e.Number)
               .IsRequired(false);

        builder.Property(e => e.Skill)
               .IsRequired(false);

        builder.Property(e => e.PhysicalCondition)
               .IsRequired(false);

        builder.Property(e => e.WeakFoot)
               .IsRequired(false);

        builder.HasOne<FootballPosition>()
               .WithMany()
               .HasForeignKey(t => t.PositionId)
               .IsRequired(false);

        builder.HasOne<FootballPosition>()
               .WithMany()
               .HasForeignKey(t => t.AdditionalPositionId)
               .IsRequired(false);

        builder.HasOne<GameStyle>()
               .WithMany()
               .HasForeignKey(t => t.GameStyleId)
               .IsRequired(false);

        builder.HasOne<WorkingFoot>()
               .WithMany()
               .HasForeignKey(t => t.WorkingFootId)
               .IsRequired(false);

        builder.ToTable("FootballProfiles");
    }
}
