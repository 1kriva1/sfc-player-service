using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFC.Players.Domain.Common;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class BaseAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseAuditableEntity
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
