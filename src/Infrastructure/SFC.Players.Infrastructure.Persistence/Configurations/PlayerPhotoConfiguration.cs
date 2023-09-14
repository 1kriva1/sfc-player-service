using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class PlayerPhotoConfiguration : IEntityTypeConfiguration<PlayerPhoto>
{
    public void Configure(EntityTypeBuilder<PlayerPhoto> builder)
    {
        builder.Property(e => e.Source)
           .HasColumnType("image")
           .IsRequired(true);

        builder.Property(e => e.Extension)
          .HasConversion<string>()
          .IsRequired(true);

        builder.Property(e => e.Name)
          .IsRequired(true);

        builder.Property(e => e.Size)
          .IsRequired(true);
    }
}