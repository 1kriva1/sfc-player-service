namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersProfileFilterDto
{
    public GetPlayersGeneralProfileFilterDto General { get; set; } = default!;

    public GetPlayersFootballProfileFilterDto Football { get; set; } = default!;
}
