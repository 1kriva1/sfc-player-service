using SFC.Players.Application.Features.Common.Models.Paging;

namespace SFC.Players.Application.Interfaces.Persistence;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<PagedList<T>> GetPageAsync(PageParameters<T> parameters);

    Task<T> AddAsync(T entity);

    Task<T[]> AddRangeAsync(params T[] entities);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
