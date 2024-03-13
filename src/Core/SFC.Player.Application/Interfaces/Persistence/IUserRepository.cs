using SFC.Player.Domain.Entities;

namespace SFC.Player.Application.Interfaces.Persistence;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<bool> AnyAsync(Guid userId);

    Task<bool> AnyAsync(long playerId, Guid userId);
}
