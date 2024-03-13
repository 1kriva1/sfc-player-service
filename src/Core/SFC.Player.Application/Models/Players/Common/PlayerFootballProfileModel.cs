using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerFootballProfileModel : IMapFrom<PlayerFootballProfileDto>
{
    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int? Position { get; set; }

    public int? AdditionalPosition { get; set; }

    public int? WorkingFoot { get; set; }

    public int? Number { get; set; }

    public int? GameStyle { get; set; }

    public int? Skill { get; set; }

    public int? WeakFoot { get; set; }

    public int? PhysicalCondition { get; set; }
}
