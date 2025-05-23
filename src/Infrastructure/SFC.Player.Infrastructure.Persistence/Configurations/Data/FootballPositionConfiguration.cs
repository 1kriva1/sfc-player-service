using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class FootballPositionConfiguration : EnumDataEntityConfiguration<FootballPosition, FootballPositionEnum>
{
    public override void Configure(EntityTypeBuilder<FootballPosition> builder)
    {
        builder.ToTable("FootballPositions", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}
