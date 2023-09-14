using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerTagConfiguration : IEntityTypeConfiguration<PlayerTag>
{
    public void Configure(EntityTypeBuilder<PlayerTag> builder)
    {
        builder.Property(e => e.Value)
            .HasMaxLength(ValidationConstants.TAG_VALUE_MAX_LENGTH)
            .IsRequired(true);
    }
}
