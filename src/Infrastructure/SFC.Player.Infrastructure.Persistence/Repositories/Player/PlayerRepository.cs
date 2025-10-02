using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Features.Common.Models.Find;
using SFC.Player.Application.Features.Common.Models.Find.Paging;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;
using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Player;
public class PlayerRepository(PlayerDbContext context)
    : Repository<PlayerEntity, PlayerDbContext, long>(context), IPlayerRepository
{
    public override Task<PlayerEntity?> GetByIdAsync(long id)
    {
        return Context.Players
                      .IncludePlayer()
                      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override Task<PagedList<PlayerEntity>> FindAsync(FindParameters<PlayerEntity> parameters)
    {
        return Context.Players
                      .IncludePlayer()
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public async Task<PlayerEntity[]> AddRangeIfNotExistsAsync(params PlayerEntity[] entities)
    {
        await Context.Set<PlayerEntity>().AddRangeIfNotExistsAsync<PlayerEntity, long>(entities).ConfigureAwait(false);

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return entities;
    }

    public async Task<PlayerEntity?> GetByUserIdAsync(Guid userId)
    {
        return await Context.Players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Photo)
            .FirstOrDefaultAsync(p => p.UserId == userId)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<PlayerEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds)
    {
        return await Context.Players
                            .IncludePlayer()
                            .Where(player => userIds.Contains(player.UserId))
                            .ToListAsync()
                            .ConfigureAwait(false);
    }

    public Task<bool> AnyAsync(Guid userId)
    {
        return Context.Players.AnyAsync(p => p.UserId == userId);
    }

    public Task<bool> AnyAsync(long id, Guid userId)
    {
        return Context.Players.AnyAsync(p => p.Id == id && p.UserId == userId);
    }

    public override async Task UpdateAsync(PlayerEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync()
                     .ConfigureAwait(false);
    }
}