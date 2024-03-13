using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Common;
public class BaseDataEntity : BaseEntity<int>, IExternalAuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}
