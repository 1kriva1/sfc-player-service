using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Data;
public class StatCategory : EnumDataEntity<StatCategoryEnum>
{
    public StatCategory() : base() { }

    public StatCategory(StatCategoryEnum enumType) : base(enumType) { }

    public ICollection<StatType> Types { get; init; } = [];
}
