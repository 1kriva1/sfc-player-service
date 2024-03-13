using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto.Pagination;

namespace SFC.Player.Application.Features.Common.Models.Paging;
public class Pagination : IMapFrom<PaginationDto>
{
    public int Page { get; set; }

    public int Size { get; set; }
}
