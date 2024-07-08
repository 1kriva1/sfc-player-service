using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public abstract class BaseDataEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseDataEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedNever()
               .HasColumnOrder(0)
               .IsRequired(true);

        builder.Property(e => e.Title)
               .HasMaxLength(DatabaseConstants.TITLE_VALUE_MAX_LENGTH)
               .IsRequired(true);

        builder.Property(e => e.CreatedDate)
               .HasColumnOrder(1)
               .IsRequired(true);        
    }
}
