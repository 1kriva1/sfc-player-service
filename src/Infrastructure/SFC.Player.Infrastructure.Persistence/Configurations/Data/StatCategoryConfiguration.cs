using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class StatCategoryConfiguration : EnumDataEntityConfiguration<StatCategory, StatCategoryEnum>
{
    public override void Configure(EntityTypeBuilder<StatCategory> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .HasForeignKey(t => t.CategoryId)
               .IsRequired(true);

        builder.ToTable("StatCategories", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}
