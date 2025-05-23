using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Metadata;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataDomainConfiguration : EnumEntityConfiguration<MetadataDomain, MetadataDomainEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataDomain> builder)
    {
        builder.ToTable("Domains", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}
