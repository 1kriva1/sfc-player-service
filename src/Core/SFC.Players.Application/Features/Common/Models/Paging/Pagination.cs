using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto.Pagination;

namespace SFC.Players.Application.Features.Common.Models.Paging;
public class Pagination : IMapFrom<PaginationDto>
{
    public int Page { get; set; }

    public int Size { get; set; }
}
