using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class StatTypeConfiguration : EnumDataEntityConfiguration<StatType, StatTypeEnum>
{
    public override void Configure(EntityTypeBuilder<StatType> builder)
    {
        builder.ToTable("StatTypes", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}
