using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Metadata;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataTypeConfiguration : EnumEntityConfiguration<MetadataType, MetadataTypeEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataType> builder)
    {
        builder.ToTable("Types", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}
