using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Common;
using SFC.Player.Infrastructure.Persistence.Extensions;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class DataRepository<T> : Repository<T, int>, IDataRepository<T> where T : BaseDataEntity
{
    public DataRepository(PlayerDbContext context) : base(context) { }

    public virtual Task<bool> AnyAsync(int id)
    {
        return _context.Set<T>().AnyAsync(u => u.Id == id);
    }

    public Task<T[]> ResetAsync(IEnumerable<T> entities)
    {
        _context.Clear<T>();

        return AddRangeAsync(entities.ToArray());
    }
}