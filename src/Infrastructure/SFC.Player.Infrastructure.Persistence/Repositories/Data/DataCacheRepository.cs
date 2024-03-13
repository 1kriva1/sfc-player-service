using System.Collections.Generic;

using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class DataCacheRepository<T> : BaseDataCacheRepository<T>, IDataRepository<T> where T : BaseDataEntity
{
    private readonly IDataRepository<T> _repository;

    public DataCacheRepository(DataRepository<T> repository, ICache cache) : base(repository, cache)
    {
        _repository = repository;
    }

    public Task<bool> AnyAsync(int id)
    {
        return _cache.TryGet(CacheKey, out IReadOnlyList<T> list)
            ? Task.FromResult(list.Any(u => u.Id == id))
            : _repository.AnyAsync(id);
    }

    public Task<T[]> ResetAsync(IEnumerable<T> entities) => _repository.ResetAsync(entities);
}
