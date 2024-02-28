namespace SFC.Players.Application.Models.Base;
public class BaseListResponse<T> : BaseErrorResponse
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
