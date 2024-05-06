using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Domain.Events;
public class GetPlayersEvent : BaseEvent
{
    public GetPlayersEvent(IEnumerable<PlayerEntity> players)
    {
        Players = players;
    }

    public IEnumerable<PlayerEntity> Players { get; }
}
