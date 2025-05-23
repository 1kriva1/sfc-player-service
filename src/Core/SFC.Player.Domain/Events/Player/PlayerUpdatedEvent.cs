namespace SFC.Player.Domain.Events.Player;

using SFC.Player.Domain.Common;

public class PlayerUpdatedEvent(PlayerEntity player) : BaseEvent
{
    public PlayerEntity Player { get; } = player;
}