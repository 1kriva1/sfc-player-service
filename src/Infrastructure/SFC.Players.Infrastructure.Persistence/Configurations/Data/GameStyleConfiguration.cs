using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class GameStyleConfiguration : BaseDataEntityConfiguration<GameStyle>
{
    public override void Configure(EntityTypeBuilder<GameStyle> builder)
    {
        builder.ToTable(nameof(GameStyle), DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
