using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class BaseEntityConfiguration<TEntity, I> 
    : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<I> where I : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd()
               .HasColumnOrder(0)
               .IsRequired(true);
    }
}
