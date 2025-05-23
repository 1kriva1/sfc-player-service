using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Common;
public class DataEntity<TId> : BaseEntity<TId>, IDataEntity
{
    public DateTime CreatedDate { get; set; }
}
