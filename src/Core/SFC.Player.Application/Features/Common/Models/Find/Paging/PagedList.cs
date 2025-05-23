namespace SFC.Player.Application.Features.Common.Models.Find.Paging;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public PagedList(IEnumerable<T> items, int count, Pagination pagination)
    {
        ArgumentNullException.ThrowIfNull(pagination);

        TotalCount = count;
        PageSize = pagination.Size;
        CurrentPage = pagination.Page;
        TotalPages = (int)Math.Ceiling(count / (double)pagination.Size);
        AddRange(items);
    }
}
