namespace SFC.Player.Messages.Events.Player.General;
public class PlayersSeeded
{
    public IEnumerable<PlayerEntity> Players { get; init; } = [];
}
