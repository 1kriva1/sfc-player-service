using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class FootballPositionConfiguration : BaseDataEntityConfiguration<FootballPosition>
{
    public override void Configure(EntityTypeBuilder<FootballPosition> builder)
    {
        builder.ToTable(nameof(FootballPosition), DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
