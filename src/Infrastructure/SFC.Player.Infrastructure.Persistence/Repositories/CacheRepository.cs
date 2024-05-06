using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Persistence;

namespace SFC.Player.Infrastructure.Persistence.Repositories;
public class CacheRepository<T, I> : IRepository<T, I> where T : class
{
    private readonly IRepository<T, I> _repository;
    protected readonly ICache _cache;
    protected virtual string CacheKey { get => $"{typeof(T).Name}"; }

    public CacheRepository(Repository<T, I> repository, ICache cache)
    {
        _repository = repository;
        _cache = cache;
    }    

    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        if (!_cache.TryGet(CacheKey, out IReadOnlyList<T> list))
        {
            list = await _repository.ListAllAsync();
            await _cache.SetAsync<IReadOnlyList<T>>(CacheKey, list);
        }

        return list;
    }

    public Task<T?> GetByIdAsync(I id) => _repository.GetByIdAsync(id);

    public Task<T> AddAsync(T entity) => _repository.AddAsync(entity);

    public Task DeleteAsync(T entity) => _repository.DeleteAsync(entity);

    public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);

    public Task<PagedList<T>> FindAsync(FindParameters<T> parameters) => _repository.FindAsync(parameters);

    public Task<T[]> AddRangeAsync(params T[] entities) => _repository.AddRangeAsync(entities);
}
