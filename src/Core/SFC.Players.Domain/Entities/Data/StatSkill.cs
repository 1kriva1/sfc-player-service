namespace SFC.Players.Domain.Entities.Data;
public class StatSkill : BaseDataEntity
{
    public ICollection<StatType> Types { get; set; } = new List<StatType>();
}
