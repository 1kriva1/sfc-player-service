namespace SFC.Players.Application.Features.Common.Models.Paging;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public PagedList(List<T> items, int count, Pagination pagination)
    {
        TotalCount = count;
        PageSize = pagination.Size;
        CurrentPage = pagination.Page;
        TotalPages = (int)Math.Ceiling(count / (double)pagination.Size);
        AddRange(items);
    }
}
