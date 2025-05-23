using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Application.Interfaces.Persistence.Repository.Common;

namespace SFC.Player.Application.Interfaces.Persistence.Repository.Player;
public interface IPlayerRepository : IRepository<PlayerEntity, IPlayerDbContext, long>
{
    Task<bool> AnyAsync(Guid userId);

    Task<bool> AnyAsync(long id, Guid userId);

    Task<PlayerEntity?> GetByUserIdAsync(Guid userId);

    Task<IEnumerable<PlayerEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds);

    Task<PlayerEntity[]> AddRangeIfNotExistsAsync(params PlayerEntity[] entities);
}
