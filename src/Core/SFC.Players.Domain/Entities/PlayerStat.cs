namespace SFC.Players.Domain.Entities;
public class PlayerStat : BasePlayerEntity
{
    public int TypeId { get; set; } = default!;

    public int CategoryId { get; set; } = default!;

    public byte Value { get; set; }
}
