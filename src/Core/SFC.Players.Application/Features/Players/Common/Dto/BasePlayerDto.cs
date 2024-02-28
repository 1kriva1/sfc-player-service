namespace SFC.Players.Application.Features.Players.Common.Dto;
public class BasePlayerDto
{
    public PlayerProfileDto Profile { get; set; } = null!;

    public PlayerStatsDto Stats { get; set; } = null!;
}