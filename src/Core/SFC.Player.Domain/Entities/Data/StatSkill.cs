using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Data;
public class StatSkill : EnumDataEntity<StatSkillEnum>
{
    public StatSkill() : base() { }

    public StatSkill(StatSkillEnum enumType) : base(enumType) { }

    public ICollection<StatType> Types { get; init; } = [];
}
