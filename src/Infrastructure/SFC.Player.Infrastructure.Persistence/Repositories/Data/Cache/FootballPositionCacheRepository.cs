using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data.Cache;
public class FootballPositionCacheRepository(FootballPositionRepository repository, ICache cache)
    : DataCacheRepository<FootballPosition, FootballPositionEnum>(repository, cache), IFootballPositionRepository
{ }
