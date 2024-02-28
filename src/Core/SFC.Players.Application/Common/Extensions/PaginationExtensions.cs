using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Interfaces.Common;

namespace SFC.Players.Application.Common.Extensions;
public static class PaginationExtensions
{
    public static PageMetadataDto SetLinks(this PageMetadataDto page, IUriService uriService, string queryString, string route)
    {
        page.Links = new PageLinksDto
        {
            FirstPage = uriService.GetPageUri(queryString, route, 1),
            LastPage = uriService.GetPageUri(queryString, route, page.TotalPages),
            NextPage = page.HasNextPage
                ? uriService.GetPageUri(queryString, route, page.CurrentPage + 1)
                : null,
            PreviousPage = page.HasPreviousPage
                ? uriService.GetPageUri(queryString, route, page.CurrentPage - 1)
                : null
        };

        return page;
    }
}
