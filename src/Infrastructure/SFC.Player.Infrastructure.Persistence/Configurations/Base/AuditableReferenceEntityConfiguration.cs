using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFC.Player.Domain.Common.Interfaces;
using SFC.Player.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Base;

public class AuditableReferenceEntityConfiguration<TEntity, TId> : BaseReferenceEntityConfiguration<TEntity, TId>
    where TEntity : BaseEntity<TId>, IAuditableReferenceEntity
    where TId : struct
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.CreatedDate)
               .IsRequired(true);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.CreatedBy)
               .OnDelete(DeleteBehavior.ClientCascade)
               .IsRequired(true);

        builder.Property(e => e.LastModifiedDate)
               .IsRequired(true);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.LastModifiedBy)
               .OnDelete(DeleteBehavior.ClientCascade)
               .IsRequired(true);

        base.Configure(builder);
    }
}