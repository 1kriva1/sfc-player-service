using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Domain.Events;
public class PlayersByFiltersEvent : BaseEvent
{
    public PlayersByFiltersEvent(IEnumerable<PlayerEntity> players)
    {
        Players = players;
    }

    public IEnumerable<PlayerEntity> Players { get; }
}
