using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PlayersDbContext dbContext) : base(dbContext) { }

    public Task<bool> AnyAsync(Guid userId)
    {
        return _dbContext.Users.AnyAsync(u=>u.UserId == userId);
    }

    public Task<bool> AnyAsync(long playerId, Guid userId)
    {
        return _dbContext.Users.AnyAsync(u => u.Player.Id == playerId && u.UserId == userId);
    }
}
