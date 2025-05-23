using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Contexts;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;

public class StatTypeRepository(DataDbContext context) 
    : DataRepository<StatType, StatTypeEnum>(context), IStatTypeRepository
{
    public virtual Task<int> CountAsync() => Context.StatTypes.CountAsync();

    public override async Task<IReadOnlyList<StatType>> ListAllAsync()
    {
        return await Context.StatTypes
                            .AsNoTracking()
                            .ToListAsync()
                            .ConfigureAwait(false);
    }
}
