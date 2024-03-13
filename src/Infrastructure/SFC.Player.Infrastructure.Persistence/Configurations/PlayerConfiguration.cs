using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerEntity> builder)
    {
        builder.HasOne(e => e.GeneralProfile)
           .WithOne(e => e.Player)
           .HasForeignKey<PlayerGeneralProfile>()
           .IsRequired();

        builder.HasOne(e => e.FootballProfile)
           .WithOne(e => e.Player)
           .HasForeignKey<PlayerFootballProfile>()
           .IsRequired();

        builder.HasOne(e => e.Availability)
           .WithOne(e => e.Player)
           .HasForeignKey<PlayerAvailability>()
           .IsRequired();

        builder.HasOne(e => e.Photo)
           .WithOne(e => e.Player)
           .HasForeignKey<PlayerPhoto>()
           .IsRequired();

        builder.HasMany(e => e.Tags)
           .WithOne(e => e.Player)
           .HasForeignKey(DbConstants.PLAYER_FOREIGN_KEY);

        builder.HasOne(e => e.Points)
           .WithOne(e => e.Player)
           .HasForeignKey<PlayerStatPoints>()
           .IsRequired();

        builder.HasMany(e => e.Stats)
           .WithOne(e => e.Player)
           .HasForeignKey(DbConstants.PLAYER_FOREIGN_KEY);

        builder.HasOne(e => e.User)
           .WithOne(e => e.Player)
           .HasForeignKey<User>()
           .IsRequired();
    }
}
