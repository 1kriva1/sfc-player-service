namespace SFC.Player.Application.Features.Player.Common;
public class BasePlayerModel
{
    public PlayerProfileModel Profile { get; set; } = null!;

    public PlayerStatsModel Stats { get; set; } = null!;
}
