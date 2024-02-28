using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersStatsFilterDto : IMapFrom<GetPlayersByFiltersStatsFilterModel>
{
    public RangeLimitDto<short?> Total { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Physical { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Mental { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitDto Skill { get; set; } = default!;

    public int? Raiting { get; set; }
}
