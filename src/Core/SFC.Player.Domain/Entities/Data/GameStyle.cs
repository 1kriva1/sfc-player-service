using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Data;
public class GameStyle : EnumDataEntity<GameStyleEnum>
{
    public GameStyle() : base() { }

    public GameStyle(GameStyleEnum enumType) : base(enumType) { }
}
