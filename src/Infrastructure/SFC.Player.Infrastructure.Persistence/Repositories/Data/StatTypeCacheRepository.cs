using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class StatTypeCacheRepository : BaseDataCacheRepository<StatType>, IStatTypeRepository
{
    private readonly IStatTypeRepository _repository;

    public StatTypeCacheRepository(StatTypeRepository repository, ICache cache) : base(repository, cache)
    {
        _repository = repository;
    }

    public Task<int> CountAsync()
    {
        return !_cache.TryGet(CacheKey, out IReadOnlyList<StatType> entities) ? _repository.CountAsync() : Task.FromResult(entities.Count);
    }
}
