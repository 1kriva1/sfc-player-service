using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto.Pagination;

namespace SFC.Player.Application.Models.Common.Pagination;
public class PageMetadataModel : IMapFrom<PageMetadataDto>
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public PageLinksModel Links { get; set; } = default!;
}
