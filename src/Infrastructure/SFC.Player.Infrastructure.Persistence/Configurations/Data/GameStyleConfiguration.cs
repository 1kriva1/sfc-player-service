using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class GameStyleConfiguration : BaseDataEntityConfiguration<GameStyle>
{
    public override void Configure(EntityTypeBuilder<GameStyle> builder)
    {
        builder.ToTable("GameStyles", DatabaseConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
