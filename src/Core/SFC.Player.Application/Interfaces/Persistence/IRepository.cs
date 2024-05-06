using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Paging;

namespace SFC.Player.Application.Interfaces.Persistence;

public interface IRepository<T, I> where T : class
{
    Task<T?> GetByIdAsync(I id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<PagedList<T>> FindAsync(FindParameters<T> parameters);

    Task<T> AddAsync(T entity);

    Task<T[]> AddRangeAsync(params T[] entities);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
