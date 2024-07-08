using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Data;
public class WorkingFootConfiguration : BaseDataEntityConfiguration<WorkingFoot>
{
    public override void Configure(EntityTypeBuilder<WorkingFoot> builder)
    {
        builder.ToTable("WorkingFoots", DatabaseConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
