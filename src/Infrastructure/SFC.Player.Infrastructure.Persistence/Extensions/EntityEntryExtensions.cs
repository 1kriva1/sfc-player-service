using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

    public static async Task<EntityEntry<T>?> AddIfNotExistsAsync<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>>? predicate = null)
       where T : class, new()
    {
        bool exists = predicate != null ? await dbSet.AnyAsync(predicate).ConfigureAwait(true) : await dbSet.AnyAsync().ConfigureAwait(true);
        return !exists ? await dbSet.AddAsync(entity).ConfigureAwait(true) : null;
    }

    public static async Task<T[]> AddRangeIfNotExistsAsync<T, TId>(this DbSet<T> dbSet, T[] entities)
        where T : BaseEntity<TId>
    {
        IEnumerable<TId> ids = entities.Select(entity => entity.Id);

        List<TId> existings = await dbSet.Where(entity => ids.Contains(entity.Id))
                                         .Select(entity => entity.Id)
                                         .ToListAsync()
                                         .ConfigureAwait(true);

        IEnumerable<T> newEntities = entities.Where(entity => !existings.Contains(entity.Id));

#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection
        if (newEntities.Any())
        {
            await dbSet.AddRangeAsync(newEntities).ConfigureAwait(true);
        }

        return newEntities.ToArray();
#pragma warning restore CA1851 // Possible multiple enumerations of 'IEnumerable' collection
    }
}
