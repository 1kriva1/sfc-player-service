using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

using IdentityUser = SFC.Player.Domain.Entities.Identity.User;
using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Common.Extensions;
public static class PlayerExtensions
{
    public static PlayerEntity SetUser(this PlayerEntity player, Guid userId)
    {
        player.User = new User
        {
            Id = userId,
            IdentityUser = new IdentityUser { Id = userId }
        };

        return player;
    }

    public static PlayerEntity SetStatTypes(this PlayerEntity player, List<StatType> statTypes)
    {
        foreach (PlayerStat item in player.Stats)
        {
            item.Type = statTypes.FirstOrDefault(r => r.Id == item.Type.Id)!;
        }

        return player;
    }
}
