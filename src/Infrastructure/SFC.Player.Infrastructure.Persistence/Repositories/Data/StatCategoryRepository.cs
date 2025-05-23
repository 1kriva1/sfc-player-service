using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Contexts;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class StatCategoryRepository(DataDbContext context) 
    : DataRepository<StatCategory, StatCategoryEnum>(context), IStatCategoryRepository
{ }
