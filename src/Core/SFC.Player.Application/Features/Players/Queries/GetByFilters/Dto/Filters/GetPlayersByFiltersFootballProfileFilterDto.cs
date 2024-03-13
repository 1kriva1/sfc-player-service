using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
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
