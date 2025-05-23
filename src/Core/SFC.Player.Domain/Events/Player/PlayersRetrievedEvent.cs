using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Events.Player;
public class PlayersRetrievedEvent(IEnumerable<PlayerEntity> players) : BaseEvent
{
    public IEnumerable<PlayerEntity> Players { get; } = players;
}
