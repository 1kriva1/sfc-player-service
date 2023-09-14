namespace SFC.Players.Domain.Events;
public class PlayerCreatedEvent : BaseEvent
{
    public PlayerCreatedEvent(Player player)
    {
        Player = player;
    }

    public Player Player { get; }
}
