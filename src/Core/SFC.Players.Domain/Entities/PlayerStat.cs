namespace SFC.Players.Domain.Entities;
public class PlayerStat : BasePlayerEntity
{
    public StatType Type { get; set; }

    public StatCategory Category { get; set; }

    public byte Value { get; set; }
}
