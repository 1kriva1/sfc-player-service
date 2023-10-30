namespace SFC.Players.Domain.Entities;
public class PlayerFootballProfile : BasePlayerEntity
{
    public short? Height { get; set; }

    public short? Weight { get; set; }

    public int? PositionId { get; set; }

    public int? AdditionalPositionId { get; set; }

    public int? WorkingFootId { get; set; }

    public short? Number { get; set; }

    public int? GameStyleId { get; set; }

    public byte? Skill { get; set; }

    public byte? WeakFoot { get; set; }

    public byte? PhysicalCondition { get; set; }
}
