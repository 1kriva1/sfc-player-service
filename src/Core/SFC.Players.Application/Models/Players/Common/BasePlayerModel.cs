namespace SFC.Players.Application.Models.Players.Common;
public class BasePlayerModel
{
    public PlayerProfileModel Profile { get; set; } = null!;

    public PlayerStatsModel Stats { get; set; } = null!;
}
