using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Configurations.Base;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class WorkingFootConfiguration : EnumDataEntityConfiguration<WorkingFoot, WorkingFootEnum>
{
    public override void Configure(EntityTypeBuilder<WorkingFoot> builder)
    {
        builder.ToTable("WorkingFoots", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}
