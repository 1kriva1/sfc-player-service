using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Common;
using SFC.Players.Infrastructure.Persistence.Extensions;

namespace SFC.Players.Infrastructure.Persistence.Repositories;
public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : BaseDataEntity
{
    public DataRepository(PlayersDbContext context) : base(context) { }

    public Task<bool> AnyAsync(int id)
    {
        return _context.Set<T>().AnyAsync(u => u.Id == id);
    }

    public Task ResetAsync(IEnumerable<T> entities)
    {
        _context.Clear<T>();

        return AddRangeAsync(entities.ToArray());
    }
}
