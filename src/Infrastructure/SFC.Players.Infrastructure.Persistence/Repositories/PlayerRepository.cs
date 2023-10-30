using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class PlayerRepository : Repository<Player>, IPlayerRepository
{
    public PlayerRepository(PlayersDbContext context) : base(context) { }

    public override async Task<Player?> GetByIdAsync(long id)
    {
        Player? player = await _context.Players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Availability)
            .Include(p => p.Availability.Days)
            .Include(p => p.Points)
            .Include(p => p.Tags)
            .Include(p => p.Stats)
            .Include(p => p.Photo)
            .FirstOrDefaultAsync(p => p.Id == id);

        return player;
    }

    public async Task<Player?> GetByUserIdAsync(Guid userId)
    {
        Player? player = await _context.Players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Photo)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.User.UserId == userId);

        return player;
    }
}
