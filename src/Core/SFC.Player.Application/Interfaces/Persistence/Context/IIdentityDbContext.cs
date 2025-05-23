using System.Linq;

using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Application.Interfaces.Persistence.Context;
public interface IIdentityDbContext : IDbContext 
{
    IQueryable<User> Users { get; }
}
