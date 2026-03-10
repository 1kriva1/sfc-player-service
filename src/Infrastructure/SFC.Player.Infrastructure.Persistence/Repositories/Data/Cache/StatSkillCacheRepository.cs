using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data.Cache;
public class StatSkillCacheRepository(StatSkillRepository repository, [FromKeyedServices(CacheInstance.Data)] ICache cache)
    : DataCacheRepository<StatSkill, StatSkillEnum>(repository, cache), IStatSkillRepository
{ }