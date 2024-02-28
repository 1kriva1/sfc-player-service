using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
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
