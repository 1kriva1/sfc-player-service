using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class StatCategoryRepository : IStatCategoryRepository
{
    private readonly PlayersDbContext _context;

    public StatCategoryRepository(PlayersDbContext context) { _context = context; }

    public Task<int> CountAsync(IEnumerable<int> categories)
    {
        return _context.StatCategories
                         .Where(c => categories.Contains(c.Id))
                         .CountAsync();
    }
}
