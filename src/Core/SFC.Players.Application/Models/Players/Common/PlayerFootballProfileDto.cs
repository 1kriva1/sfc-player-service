using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Models.Players.Common;

public class PlayerFootballProfileDto: IMapFromReverse<PlayerFootballProfile>
{
    public int? Height { get; set; }

    public int? Weight { get; set; }

    public FootballPosition? Position { get; set; }

    public FootballPosition? AdditionalPosition { get; set; }

    public WorkingFoot? WorkingFoot { get; set; }

    public int? Number { get; set; }

    public GameStyle? GameStyle { get; set; }

    public int? Skill { get; set; }

    public int? WeakFoot { get; set; }

    public int? PhysicalCondition { get; set; }
}
