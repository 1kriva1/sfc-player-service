namespace SFC.Player.Domain.Entities.Data;
public class StatCategory : BaseDataEntity
{
    public ICollection<StatType> Types { get; set; } = new List<StatType>();
}
