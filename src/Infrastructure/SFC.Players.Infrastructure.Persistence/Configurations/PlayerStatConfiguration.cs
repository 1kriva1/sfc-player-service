using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerStatConfiguration : IEntityTypeConfiguration<PlayerStat>
{
    public void Configure(EntityTypeBuilder<PlayerStat> builder)
    {
        builder.Property(e => e.Type)
           .HasConversion<byte>()
           .IsRequired(true);

        builder.Property(e => e.Category)
           .HasConversion<byte>()
           .IsRequired(true);
    }
}
