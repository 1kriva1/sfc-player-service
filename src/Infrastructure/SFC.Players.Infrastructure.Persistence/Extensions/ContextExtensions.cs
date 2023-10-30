using Microsoft.EntityFrameworkCore;

namespace SFC.Players.Infrastructure.Persistence.Extensions;
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
}
