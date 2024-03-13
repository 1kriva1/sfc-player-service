using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class PlayerGeneralProfileConfiguration : IEntityTypeConfiguration<PlayerGeneralProfile>
{
    public void Configure(EntityTypeBuilder<PlayerGeneralProfile> builder)
    {
        builder.Property(e => e.FirstName)
            .HasMaxLength(ValidationConstants.NAME_VALUE_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(e => e.LastName)
            .HasMaxLength(ValidationConstants.NAME_VALUE_MAX_LENGTH)
            .IsRequired(true);        

        builder.Property(e => e.Biography)
            .HasMaxLength(ValidationConstants.DESCRIPTION_VALUE_MAX_LENGTH)
            .IsRequired(false);

        builder.Property(e => e.Birthday)
            .HasColumnType("date")
            .IsRequired(false);

        builder.Property(e => e.City)
            .HasMaxLength(ValidationConstants.CITY_VALUE_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(e => e.FreePlay)
            .HasDefaultValue(false);
    }
}
