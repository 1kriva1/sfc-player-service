using SFC.Player.Domain.Entities.Metadata;

namespace SFC.Player.Application.Interfaces.Persistence.Context;
public interface IMetadataDbContext : IDbContext
{
    IQueryable<MetadataEntity> Metadata { get; }
}
