using System.Collections.Generic;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class BaseDataCacheRepository<T> : CacheRepository<T, int> where T : class
{
    private readonly IRepository<T, int> _repository;

    protected override string CacheKey => $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{base.CacheKey}";

    public BaseDataCacheRepository(Repository<T, int> repository, ICache cache) : base(repository, cache)
    {
        _repository = repository;
    }    

    public override Task<IReadOnlyList<T>> ListAllAsync()
    {
        return !_cache.TryGet(CacheKey, out IReadOnlyList<T> list) ? _repository.ListAllAsync() : Task.FromResult(list);
    }
}
