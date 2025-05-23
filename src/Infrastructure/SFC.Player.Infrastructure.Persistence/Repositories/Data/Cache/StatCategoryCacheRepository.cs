using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data.Cache;
public class StatCategoryCacheRepository(StatCategoryRepository repository, ICache cache)
    : DataCacheRepository<StatCategory, StatCategoryEnum>(repository, cache), IStatCategoryRepository
{ }
