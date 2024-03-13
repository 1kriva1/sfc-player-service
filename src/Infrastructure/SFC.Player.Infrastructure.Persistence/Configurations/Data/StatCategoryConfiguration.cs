using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class StatCategoryConfiguration : BaseDataEntityConfiguration<StatCategory>
{
    public override void Configure(EntityTypeBuilder<StatCategory> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .IsRequired(true);

        builder.ToTable("StatCategories", DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
