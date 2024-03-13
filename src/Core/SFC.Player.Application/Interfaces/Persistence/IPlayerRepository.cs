using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Interfaces.Persistence;
public interface IPlayerRepository : IRepository<PlayerEntity, long>
{
    Task<PlayerEntity?> GetByUserIdAsync(Guid userId);
}
