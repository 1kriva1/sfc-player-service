using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Domain.Entities;
public class PlayerStat : BasePlayerEntity
{
    public StatType Type { get; set; } = default!;

    public byte Value { get; set; }
}
