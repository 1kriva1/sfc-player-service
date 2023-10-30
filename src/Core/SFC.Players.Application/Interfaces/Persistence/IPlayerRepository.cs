using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Interfaces.Persistence;

public interface IPlayerRepository : IRepository<Player>
{
    Task<Player?> GetByUserIdAsync(Guid userId);
}
