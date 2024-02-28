using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class StatTypeRepository : IStatTypeRepository
{
    private readonly PlayersDbContext _context;

    public StatTypeRepository(PlayersDbContext context) { _context = context; }

    public Task<int> CountAsync()
    {
        return _context.StatTypes.CountAsync();
    }

    public Task<int> CountAsync(IEnumerable<int> types)
    {
        return _context.StatTypes
                         .Where(c => types.Contains(c.Id))
                         .CountAsync();
    }

    public async Task<IReadOnlyList<StatType>> ListAllAsync()
    {
        return await _context.Set<StatType>()
                             .AsNoTracking()
                             .ToListAsync();
    }
}
