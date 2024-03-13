namespace SFC.Player.Domain.Events;
using PlayerEntity = SFC.Player.Domain.Entities.Player;
public class PlayerCreatedEvent : BaseEvent
{
    public PlayerCreatedEvent(PlayerEntity player)
    {
        Player = player;
    }

    public PlayerEntity Player { get; }
}
