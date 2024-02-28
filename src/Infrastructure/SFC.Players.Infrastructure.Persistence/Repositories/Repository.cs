using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Features.Common.Models.Paging;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Infrastructure.Persistence.Extensions;

namespace SFC.Players.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly PlayersDbContext _context;

    public Repository(PlayersDbContext context) { _context = context; }

    public virtual async Task<T?> GetByIdAsync(long id)
    {
        T? t = await _context.Set<T>().FindAsync(id);
        return t;
    }

    public virtual async Task<PagedList<T>> GetPageAsync(PageParameters<T> parameters)
    {
        return await _context.Set<T>()
                             .AsQueryable<T>()
                             .PaginateAsync(parameters);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T[]> AddRangeAsync(params T[] entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }    
}
