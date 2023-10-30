namespace SFC.Players.Application.Interfaces.Persistence;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T> AddAsync(T entity);

    Task<T[]> AddRangeAsync(params T[] entities);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
