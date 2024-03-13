using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto.Pagination;

namespace SFC.Player.Application.Models.Common.Pagination;
public class PageLinksModel : IMapFrom<PageLinksDto>
{
    public Uri FirstPage { get; set; } = default!;

    public Uri LastPage { get; set; } = default!;

    public Uri? NextPage { get; set; }

    public Uri? PreviousPage { get; set; }
}
