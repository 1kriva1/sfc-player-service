using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;

namespace SFC.Player.Application.Features.Player.GetByFilters.Result;
public class PlayerByFiltersFootballProfileModel : IMapFrom<PlayerByFiltersFootballProfileDto>
{
    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int? Position { get; set; }

    public int? WorkingFoot { get; set; }

    public int? GameStyle { get; set; }

    public int? Skill { get; set; }

    public int? PhysicalCondition { get; set; }
}
