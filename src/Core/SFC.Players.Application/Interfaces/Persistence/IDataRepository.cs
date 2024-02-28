using SFC.Players.Domain.Common;

namespace SFC.Players.Application.Interfaces.Persistence;
public interface IDataRepository<T> : IRepository<T> where T : BaseDataEntity
{
    Task<bool> AnyAsync(int id);

    Task<T[]> ResetAsync(IEnumerable<T> entities);    
}
