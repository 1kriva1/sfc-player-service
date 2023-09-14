using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Common.Extensions;
public static class PlayerExtensions
{
    public static Player SetUser(this Player player, Guid userId)
    {
        player.User = new User { UserId = userId };

        return player;
    }
}
