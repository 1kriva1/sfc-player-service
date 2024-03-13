using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Interfaces.Persistence;
public interface IStatTypeRepository : IRepository<StatType, int>
{
    Task<int> CountAsync();
}
