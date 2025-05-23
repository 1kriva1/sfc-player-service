using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Data;
public class FootballPosition : EnumDataEntity<FootballPositionEnum>
{
    public FootballPosition() : base() { }

    public FootballPosition(FootballPositionEnum enumType) : base(enumType) { }
}
