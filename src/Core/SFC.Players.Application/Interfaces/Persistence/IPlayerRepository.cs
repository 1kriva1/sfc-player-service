using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Interfaces.Persistence;

public interface IPlayerRepository : IAsyncRepository<Player>
{
    Task<Player?> GetByUserIdAsync(Guid userId);
}
