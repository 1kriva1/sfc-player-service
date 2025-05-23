using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Base;
public abstract class EnumDataEntityConfiguration<TEntity, TEnum> : IEntityTypeConfiguration<TEntity>
    where TEntity : EnumDataEntity<TEnum>
    where TEnum : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedNever()
               .HasColumnOrder(0)
               .IsRequired(true);

        builder.Property(e => e.Title)
               .HasMaxLength(ValidationConstants.TitleValueMaxLength)
               .IsRequired(true);

        builder.Property(e => e.CreatedDate)
               .HasColumnOrder(1)
               .IsRequired(true);
    }
}
