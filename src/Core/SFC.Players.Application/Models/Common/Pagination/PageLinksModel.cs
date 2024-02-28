using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto.Pagination;

namespace SFC.Players.Application.Models.Common.Pagination;
public class PageLinksModel : IMapFrom<PageLinksDto>
{
    public Uri FirstPage { get; set; } = default!;

    public Uri LastPage { get; set; } = default!;

    public Uri? NextPage { get; set; }

    public Uri? PreviousPage { get; set; }
}
