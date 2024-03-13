using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Domain.Common;
using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class BaseAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity: class, IAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedDate)
            .IsRequired(true);

        builder.Property(e => e.CreatedBy)
            .IsRequired(true);

        builder.Property(e => e.LastModifiedDate)
            .IsRequired(true);

        builder.Property(e => e.LastModifiedBy)
            .IsRequired(true);
    }
}
