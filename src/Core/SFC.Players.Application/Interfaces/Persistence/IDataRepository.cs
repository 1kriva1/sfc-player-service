using SFC.Players.Domain.Common;

namespace SFC.Players.Application.Interfaces.Persistence;
public interface IDataRepository<T> where T : BaseDataEntity
{
    Task<bool> AnyAsync(int id);

    Task ResetAsync(IEnumerable<T> entities);    
}
