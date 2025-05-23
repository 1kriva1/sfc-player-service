namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersFilterDto
{
    public GetPlayersProfileFilterDto Profile { get; set; } = default!;

    public GetPlayersStatsFilterDto Stats { get; set; } = default!;
}
