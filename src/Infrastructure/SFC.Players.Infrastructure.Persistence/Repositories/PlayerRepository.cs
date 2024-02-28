using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Features.Common.Models.Paging;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Extensions;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class PlayerRepository : Repository<Player>, IPlayerRepository
{
    public PlayerRepository(PlayersDbContext context) : base(context) { }

    public override async Task<Player> AddAsync(Player entity)
    {
        await _context.Set<Player>().AddAsync(entity);

        _context.SetPlayerStats(entity.Stats);

        await _context.SaveChangesAsync();

        return entity;
    }

    public override async Task<Player?> GetByIdAsync(long id)
    {
        return await _context.Players
                    .Include(p => p.GeneralProfile)
                    .Include(p => p.FootballProfile)
                    .Include(p => p.Availability)
                    .Include(p => p.Availability.Days)
                    .Include(p => p.Points)
                    .Include(p => p.Tags)
                    .Include(p => p.Stats)
                    .ThenInclude(x => x.Type)
                    .Include(p => p.Photo)
                    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<PagedList<Player>> GetPageAsync(PageParameters<Player> parameters)
    {
        return await _context.Players
                    .Include(p => p.GeneralProfile)
                    .Include(p => p.FootballProfile)
                    .Include(p => p.Availability)
                    .Include(p => p.Availability.Days)
                    .Include(p => p.Tags)
                    .Include(p => p.Stats)
                    .ThenInclude(x => x.Type)
                    .Include(p => p.Photo)
                    .AsQueryable()
                    .PaginateAsync(parameters);
    }

    public async Task<Player?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Photo)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.User.UserId == userId);
    }
}
