using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Infrastructure.Persistence.Extensions;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.Repositories;
public class PlayerRepository : Repository<PlayerEntity, long>, IPlayerRepository
{
    public PlayerRepository(PlayerDbContext context) : base(context) { }

    public override async Task<PlayerEntity> AddAsync(PlayerEntity entity)
    {
        await _context.Set<PlayerEntity>().AddAsync(entity);

        _context.SetPlayerStats(entity.Stats);

        await _context.SaveChangesAsync();

        return entity;
    }

    public override async Task<PlayerEntity?> GetByIdAsync(long id)
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

    public override async Task<PagedList<PlayerEntity>> FindAsync(FindParameters<PlayerEntity> parameters)
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

    public async Task<PlayerEntity?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Photo)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.User.Id == userId);
    }
}
