namespace SFC.Player.Domain.Events.Player;

using SFC.Player.Domain.Common;

public class PlayerCreatedEvent(PlayerEntity player) : BaseEvent
{
    public PlayerEntity Player { get; } = player;
}