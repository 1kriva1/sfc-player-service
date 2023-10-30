namespace SFC.Players.Domain.Common;
public class BaseDataEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}
