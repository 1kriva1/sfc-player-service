namespace SFC.Players.Application.Features.Common.Dto.Pagination;
public class PageMetadataDto
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;

    public PageLinksDto Links { get; set; } = default!;
}
