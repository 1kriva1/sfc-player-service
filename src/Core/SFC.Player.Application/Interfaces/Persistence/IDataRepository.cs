using SFC.Player.Domain.Common;

namespace SFC.Player.Application.Interfaces.Persistence;
public interface IDataRepository<T> : IRepository<T, int> where T : BaseDataEntity
{
    Task<bool> AnyAsync(int id);

    Task<T[]> ResetAsync(IEnumerable<T> entities);    
}
