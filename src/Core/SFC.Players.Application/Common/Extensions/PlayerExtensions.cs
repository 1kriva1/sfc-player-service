using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.Common.Extensions;
public static class PlayerExtensions
{
    public static Player SetUser(this Player player, Guid userId)
    {
        player.User = new User { UserId = userId };

        return player;
    }

    public static Player SetStatTypes(this Player player, List<StatType> statTypes)
    {
        foreach (PlayerStat item in player.Stats)
        {
            item.Type = statTypes.FirstOrDefault(r => r.Id == item.Type.Id)!;
        }

        return player;
    }
}
