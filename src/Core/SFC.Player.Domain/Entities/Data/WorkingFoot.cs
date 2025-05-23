using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Data;
public class WorkingFoot : EnumDataEntity<WorkingFootEnum>
{
    public WorkingFoot() : base() { }

    public WorkingFoot(WorkingFootEnum enumType) : base(enumType) { }
}
