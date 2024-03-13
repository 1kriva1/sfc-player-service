using Microsoft.EntityFrameworkCore;

using SFC.Player.Domain.Entities;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class ContextExtensions
{
    public static void Clear<T>(this DbContext context) where T : class
    {
        DbSet<T> dbSet = context.Set<T>();

        if (dbSet.Any())
        {
            dbSet.RemoveRange(dbSet.ToList());
        }
    }

    public static void SetPlayerStats(this DbContext context, ICollection<PlayerStat> stats, EntityState state = EntityState.Unchanged)
    {
        foreach (PlayerStat stat in stats)
        {
            context.Entry(stat.Type).State = state;
        }
    }
}
