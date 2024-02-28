using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class StatSkillConfiguration : BaseDataEntityConfiguration<StatSkill>
{
    public override void Configure(EntityTypeBuilder<StatSkill> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .IsRequired();

        builder.ToTable(nameof(StatSkill), DbConstants.DATA_SCHEMA_NAME);

        base.Configure(builder);
    }
}
