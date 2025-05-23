using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Application.Interfaces.Persistence.Repository.Common;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Application.Interfaces.Persistence.Repository.Identity;
public interface IUserRepository : IRepository<User, IIdentityDbContext, Guid>
{
    Task<User[]> AddRangeIfNotExistsAsync(params User[] entities);
}
