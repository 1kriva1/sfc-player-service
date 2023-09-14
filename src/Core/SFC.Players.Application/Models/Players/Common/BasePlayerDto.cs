namespace SFC.Players.Application.Models.Players.Common;
public class BasePlayerDto
{
    public PlayerProfileDto Profile { get; set; } = null!;

    public PlayerStatsDto Stats { get; set; } = null!;
}