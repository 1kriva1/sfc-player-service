using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Player;
public abstract class BasePlayerEntity : BaseEntity<long>
{
    public PlayerEntity Player { get; set; } = null!;
}
