using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class FootballPositionConfiguration : BaseDataEntityConfiguration<FootballPosition>
{
    public override void Configure(EntityTypeBuilder<FootballPosition> builder)
    {
        builder.ToTable("FootballPositions", DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
