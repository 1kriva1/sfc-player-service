using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersStatsFilterDto : IMapFrom<GetPlayersByFiltersStatsFilterModel>
{
    public RangeLimitDto<short?> Total { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Physical { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Mental { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Skill { get; set; } = default!;

    public int? Raiting { get; set; }
}
