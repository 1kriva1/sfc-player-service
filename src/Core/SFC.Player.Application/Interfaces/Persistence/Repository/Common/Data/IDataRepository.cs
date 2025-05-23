using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Domain.Common;

namespace SFC.Player.Application.Interfaces.Persistence.Repository.Common.Data;
public interface IDataRepository<TEntity, TContext, TEnum> : IRepository<TEntity, TContext, TEnum>
    where TEntity : EnumDataEntity<TEnum>
    where TContext : IDbContext
    where TEnum : struct
{
    Task<bool> AnyAsync(TEnum id);

    Task<TEntity[]> ResetAsync(IEnumerable<TEntity> entities);
}
