using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Interfaces.Persistence.Repository.Data;
public interface IStatTypeRepository : IDataRepository<StatType, StatTypeEnum>
{
    Task<int> CountAsync();
}
