using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersFootballProfileFilterDto : IMapFrom<GetPlayersByFiltersFootballProfileFilterModel>
{
    public RangeLimitDto<short?> Height { get; set; } = default!;

    public RangeLimitDto<short?> Weight { get; set; } = default!;

    public IEnumerable<int> Positions { get; set; } = Array.Empty<int>();

    public int? WorkingFoot { get; set; }

    public IEnumerable<int> GameStyles { get; set; } = Array.Empty<int>();

    public int? Skill { get; set; }

    public int? PhysicalCondition { get; set; }
}
