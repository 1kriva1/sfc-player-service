using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Common.Data;
public class DataCacheRepository<TEntity, TContext, TEnum>(DataRepository<TEntity, TContext, TEnum> repository, ICache cache)
    : CacheRepository<TEntity, TContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TContext : DbContext, IDbContext
     where TEnum : struct
{
    private readonly DataRepository<TEntity, TContext, TEnum> _repository = repository;

    public Task<bool> AnyAsync(TEnum id)
    {
        return Cache.TryGet(CacheKey, out IReadOnlyList<TEntity> list)
        ? Task.FromResult(list.Any(u => u.Id.Equals(id)))
            : _repository.AnyAsync(id);
    }

    public Task<TEntity[]> ResetAsync(IEnumerable<TEntity> entities) => _repository.ResetAsync(entities);
}
