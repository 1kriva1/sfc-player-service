using SFC.Player.Messages.Commands.Common;

namespace SFC.Player.Messages.Commands.Player;
public class SeedPlayers : InitiatorCommand
{
    public IEnumerable<PlayerEntity> Players { get; init; } = [];
}
