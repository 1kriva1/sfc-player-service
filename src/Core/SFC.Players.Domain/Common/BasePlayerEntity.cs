namespace SFC.Players.Domain.Common;
public abstract class BasePlayerEntity : BaseEntity
{
    public Player Player { get; set; } = null!;
}
