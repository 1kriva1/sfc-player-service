using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Metadata;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataServiceConfiguration : EnumEntityConfiguration<MetadataService, MetadataServiceEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataService> builder)
    {
        builder.ToTable("Services", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}
