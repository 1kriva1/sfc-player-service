using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerStatConfiguration : IEntityTypeConfiguration<PlayerStat>
{
    public void Configure(EntityTypeBuilder<PlayerStat> builder)
    {

        builder.HasOne<StatCategory>()
                       .WithMany()
                       .HasForeignKey(t => t.CategoryId)
                       .IsRequired(true)
                       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<StatType>()
                       .WithMany()
                       .HasForeignKey(t => t.TypeId)
                       .IsRequired(true)
                       .OnDelete(DeleteBehavior.Restrict);
    }
}
