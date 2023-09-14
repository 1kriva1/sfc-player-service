using Microsoft.EntityFrameworkCore;
using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly PlayersDbContext _dbContext;

    public BaseRepository(PlayersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(long id)
    {
        T? t = await _dbContext.Set<T>().FindAsync(id);
        return t;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
