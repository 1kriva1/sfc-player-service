namespace SFC.Player.Domain.Common;
using PlayerEntity = SFC.Player.Domain.Entities.Player;
public abstract class BasePlayerEntity : BaseEntity<long>
{
    public PlayerEntity Player { get; set; } = null!;
}
