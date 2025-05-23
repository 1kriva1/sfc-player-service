using SFC.Player.Domain.Common;

namespace SFC.Player.Application.Interfaces.Reference;
public interface IReference<TEntity, TId, TDto>
    where TEntity : BaseEntity<TId>
{
    Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);
}
