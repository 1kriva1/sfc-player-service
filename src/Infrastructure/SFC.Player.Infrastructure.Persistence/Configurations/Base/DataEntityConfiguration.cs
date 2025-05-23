using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Base;
public class DataEntityConfiguration<TEntity, TID> : IEntityTypeConfiguration<TEntity>
    where TEntity : DataEntity<TID>
    where TID : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedNever()
               .HasColumnOrder(0)
               .IsRequired(true);
    }
}