using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersFootballProfileFilterDto : IMapFrom<GetPlayersFootballProfileFilterModel>
{
    public RangeLimitDto<short?> Height { get; set; } = default!;

    public RangeLimitDto<short?> Weight { get; set; } = default!;

    public IEnumerable<int> Positions { get; set; } = Array.Empty<int>();

    public int? WorkingFoot { get; set; }

    public IEnumerable<int> GameStyles { get; set; } = Array.Empty<int>();

    public int? Skill { get; set; }

    public int? PhysicalCondition { get; set; }
}
