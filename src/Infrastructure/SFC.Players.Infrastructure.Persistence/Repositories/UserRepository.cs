using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(PlayersDbContext context) : base(context) { }

    public Task<bool> AnyAsync(Guid userId)
    {
        return _context.Users.AnyAsync(u => u.UserId == userId);
    }

    public Task<bool> AnyAsync(long playerId, Guid userId)
    {
        return _context.Users.AnyAsync(u => u.Player.Id == playerId && u.UserId == userId);
    }
}
