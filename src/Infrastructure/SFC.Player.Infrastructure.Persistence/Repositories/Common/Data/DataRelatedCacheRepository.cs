using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Common.Data;
public class DataRelatedCacheRepository<TEntity, TContext, TEnum>(DataRepository<TEntity, TContext, TEnum> repository, ICache cache)
    : DataCacheRepository<TEntity, TContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TContext : DbContext, IDbContext
     where TEnum : struct
{
    private readonly DataRepository<TEntity, TContext, TEnum> _repository = repository;

    public override async Task<IReadOnlyList<TEntity>> ListAllAsync()
    {
        if (!Cache.TryGet(CacheKey, out IReadOnlyList<TEntity> list))
        {
            list = await _repository.ListAllAsync()
                                    .ConfigureAwait(false);
        }

        return list;
    }
}