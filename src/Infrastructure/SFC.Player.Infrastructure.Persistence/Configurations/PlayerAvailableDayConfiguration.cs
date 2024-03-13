using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class PlayerAvailableDayConfiguration : IEntityTypeConfiguration<PlayerAvailableDay>
{
    public void Configure(EntityTypeBuilder<PlayerAvailableDay> builder)
    {
        builder.Property(e => e.Day)
            .HasConversion<byte>()
            .IsRequired(true);
    }
}
