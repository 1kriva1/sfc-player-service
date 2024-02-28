using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class StatTypeConfiguration : BaseDataEntityConfiguration<StatType>
{
    public override void Configure(EntityTypeBuilder<StatType> builder)
    {
        builder.HasOne(e => e.Category)
               .WithMany(e => e.Types)
               .IsRequired();

        builder.HasOne(e => e.Skill)
               .WithMany(e => e.Types)
               .IsRequired();

        builder.ToTable(nameof(StatType), DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
