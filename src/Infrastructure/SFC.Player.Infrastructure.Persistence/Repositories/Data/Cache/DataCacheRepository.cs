using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Domain.Common;
using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data.Cache;
public class DataCacheRepository<TEntity, TEnum>(DataRepository<TEntity, TEnum> repository, ICache cache)
    : DataCacheRepository<TEntity, DataDbContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }
