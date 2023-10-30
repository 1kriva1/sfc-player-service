using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.Interfaces.Persistence;
public interface IStatTypeRepository
{
    Task<IReadOnlyList<StatType>> ListAllAsync();

    Task<int> CountAsync();

    Task<int> CountAsync(IEnumerable<int> types);
}
