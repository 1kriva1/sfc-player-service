namespace SFC.Players.Domain.Entities;
public class PlayerFootballProfile : BasePlayerEntity
{
    public short? Height { get; set; }

    public short? Weight { get; set; }

    public FootballPosition? Position { get; set; }

    public FootballPosition? AdditionalPosition { get; set; }

    public WorkingFoot? WorkingFoot { get; set; }

    public short? Number { get; set; }

    public GameStyle? GameStyle { get; set; }

    public byte? Skill { get; set; }

    public byte? WeakFoot { get; set; }

    public byte? PhysicalCondition { get; set; }
}
