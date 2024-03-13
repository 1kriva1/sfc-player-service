using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(PlayerDbContext context) : base(context) { }

    public Task<bool> AnyAsync(Guid userId)
    {
        return _context.Users.AnyAsync(u => u.Id == userId);
    }

    public Task<bool> AnyAsync(long playerId, Guid userId)
    {
        return _context.Users.AnyAsync(u => u.Player.Id == playerId && u.Id == userId);
    }
}
