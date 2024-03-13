using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;
public class GetPlayersByFiltersStatsFilterModel
{
    public RangeLimitModel<short?> Total { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitModel Physical { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitModel Mental { get; set; } = default!;

    public GetPlayersByFiltersStatsBySkillRangeLimitModel Skill { get; set; } = default!;

    public int? Raiting { get; set; }
}
