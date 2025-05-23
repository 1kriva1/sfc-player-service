using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Contexts;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class FootballPositionRepository(DataDbContext context)
    : DataRepository<FootballPosition, FootballPositionEnum>(context), IFootballPositionRepository
{ }
