using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class PlayerPointsConfiguration : IEntityTypeConfiguration<PlayerStatPoints>
{
    public void Configure(EntityTypeBuilder<PlayerStatPoints> builder)
    {
        builder.Property(e => e.Available)
            .HasDefaultValue(0);

        builder.Property(e => e.Used)
            .HasDefaultValue(0);
    }
}
