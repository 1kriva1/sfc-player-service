using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerFootballProfileConfiguration : IEntityTypeConfiguration<PlayerFootballProfile>
{
    public void Configure(EntityTypeBuilder<PlayerFootballProfile> builder)
    {
        builder.Property(e => e.Height)
            .IsRequired(false);

        builder.Property(e => e.Weight)
            .IsRequired(false);

        builder.Property(e => e.Position)
            .HasConversion<byte>()
            .IsRequired(false);

        builder.Property(e => e.AdditionalPosition)
            .HasConversion<byte>()
            .IsRequired(false);

        builder.Property(e => e.WorkingFoot)
            .HasConversion<byte>()
            .IsRequired(false);

        builder.Property(e => e.Number)
           .IsRequired(false);

        builder.Property(e => e.GameStyle)
            .HasConversion<byte>()
            .IsRequired(false);

        builder.Property(e => e.Skill)
           .IsRequired(false);

        builder.Property(e => e.WeakFoot)
           .IsRequired(false);

        builder.Property(e => e.PhysicalCondition)
           .IsRequired(false);
    }
}
