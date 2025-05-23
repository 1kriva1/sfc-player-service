using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Domain.Entities.Identity;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Player.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Identity;
public class UserRepository(IdentityDbContext context)
        : Repository<User, IdentityDbContext, Guid>(context), IUserRepository
{
    public async Task<User[]> AddRangeIfNotExistsAsync(params User[] entities)
    {
        await Context.Set<User>().AddRangeIfNotExistsAsync<User, Guid>(entities).ConfigureAwait(false);

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return entities;
    }
}
