using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Configurations.Data;
public class WorkingFootConfiguration : BaseDataEntityConfiguration<WorkingFoot>
{
    public override void Configure(EntityTypeBuilder<WorkingFoot> builder)
    {
        builder.ToTable(nameof(WorkingFoot), DbConstants.DATA_SCHEMA_NAME);
        base.Configure(builder);
    }
}
