namespace SFC.Player.Domain.Entities.Data;
public class StatType : BaseDataEntity
{
    public StatCategory Category { get; set; } = default!;

    public StatSkill Skill { get; set; } = default!;
}
