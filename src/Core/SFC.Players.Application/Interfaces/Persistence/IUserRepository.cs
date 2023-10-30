using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Interfaces.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<bool> AnyAsync(Guid userId);

    Task<bool> AnyAsync(long playerId, Guid userId);
}
