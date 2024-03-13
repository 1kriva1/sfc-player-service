using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;

public class StatTypeRepository : Repository<StatType, int>, IStatTypeRepository
{
    public StatTypeRepository(PlayerDbContext context) : base(context) { }

    public virtual Task<int> CountAsync() => _context.StatTypes.CountAsync();

    public override async Task<IReadOnlyList<StatType>> ListAllAsync()
    {
        return await _context.Set<StatType>()
                             .AsNoTracking()
                             .ToListAsync();
    }
}
