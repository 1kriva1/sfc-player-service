using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Persistence.Repository.Metadata;
using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Metadata;
public class MetadataRepository(MetadataDbContext context)
    : Repository<MetadataEntity, MetadataDbContext, int>(context), IMetadataRepository
{
    public Task<MetadataEntity?> GetByIdAsync(MetadataServiceEnum service, MetadataDomainEnum domain, MetadataTypeEnum type)
    {
        return Context.Metadata.FirstOrDefaultAsync(metadata =>
            metadata.Service == service &&
            metadata.Domain == domain &&
            metadata.Type == type);
    }
}