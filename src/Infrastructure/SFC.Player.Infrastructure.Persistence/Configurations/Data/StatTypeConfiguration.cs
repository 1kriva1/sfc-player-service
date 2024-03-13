using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
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

        builder.ToTable("StatTypes", DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
