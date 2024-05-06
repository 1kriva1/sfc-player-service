using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersStatsFilterDto : IMapFrom<GetPlayersStatsFilterModel>
{
    public RangeLimitDto<short?> Total { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Physical { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Mental { get; set; } = default!;

    public GetPlayersStatsBySkillRangeLimitDto Skill { get; set; } = default!;

    public int? Raiting { get; set; }
}
