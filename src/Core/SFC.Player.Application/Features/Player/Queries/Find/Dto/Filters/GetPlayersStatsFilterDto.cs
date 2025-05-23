using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersStatsFilterDto
{
    public RangeLimitDto<short?> Total { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Physical { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Mental { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Skill { get; set; } = default!;

    public int? Raiting { get; set; }
}
