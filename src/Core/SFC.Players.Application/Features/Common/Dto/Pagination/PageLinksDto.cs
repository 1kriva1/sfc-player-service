namespace SFC.Players.Application.Features.Common.Dto.Pagination;
public class PageLinksDto
{
    public Uri FirstPage { get; set; } = default!;

    public Uri LastPage { get; set; } = default!;

    public Uri? NextPage { get; set; }

    public Uri? PreviousPage { get; set; }
}
