using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Domain.Entities;
public class PlayerStat : BasePlayerEntity
{
    public StatType Type { get; set; } = default!;

    public byte Value { get; set; }
}
