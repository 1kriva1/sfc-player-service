using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Player;
public class PlayerAvailableDayConfiguration : IEntityTypeConfiguration<PlayerAvailableDay>
{
    public void Configure(EntityTypeBuilder<PlayerAvailableDay> builder)
    {
        builder.Property(e => e.Day)
            .HasConversion<byte>()
            .IsRequired(true);

        builder.ToTable("AvailableDays");
    }
}
