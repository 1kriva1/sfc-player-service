using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Identity;
using SFC.Player.Domain.Entities.Player;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Player;
public class PlayerConfiguration : AuditableEntityConfiguration<PlayerEntity, long>
{
    public override void Configure(EntityTypeBuilder<PlayerEntity> builder)
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
               .HasForeignKey(DatabaseConstants.PlayerForeignKey);

        builder.HasOne(e => e.Points)
               .WithOne(e => e.Player)
               .HasForeignKey<PlayerStatPoints>()
               .IsRequired();

        builder.HasMany(e => e.Stats)
               .WithOne(e => e.Player)
               .HasForeignKey(DatabaseConstants.PlayerForeignKey);

        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<PlayerEntity>()
               .IsRequired(true);

        builder.ToTable("Players");

        base.Configure(builder);
    }
}
