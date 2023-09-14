using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
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
