using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class StatSkillConfiguration : BaseDataEntityConfiguration<StatSkill>
{
    public override void Configure(EntityTypeBuilder<StatSkill> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .IsRequired();

        builder.ToTable("StatSkills", DbConstants.DATA_SCHEMA_NAME);

        base.Configure(builder);
    }
}
