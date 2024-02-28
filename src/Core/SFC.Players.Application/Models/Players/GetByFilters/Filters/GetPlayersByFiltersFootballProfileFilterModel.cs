using SFC.Players.Application.Models.Common;

namespace SFC.Players.Application.Models.Players.GetByFilters.Filters;
public class GetPlayersByFiltersFootballProfileFilterModel
{
    public RangeLimitModel<short?> Height { get; set; } = default!;

    public RangeLimitModel<short?> Weight { get; set; } = default!;

    public IEnumerable<int> Positions { get; set; } = default!;

    public int? WorkingFoot { get; set; }

    public IEnumerable<int> GameStyles { get; set; } = default!;

    public int? Skill { get; set; }

    public int? PhysicalCondition { get; set; }
}
