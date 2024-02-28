using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class StatCategoryConfiguration : BaseDataEntityConfiguration<StatCategory>
{
    public override void Configure(EntityTypeBuilder<StatCategory> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .IsRequired(true);

        builder.ToTable(nameof(StatCategory), DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
