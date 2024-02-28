using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Common.Pagination;

namespace SFC.Players.Application.Features.Common.Dto.Pagination;
public class PaginationDto : IMapFrom<PaginationModel>
{
    public int Page { get; set; }

    public int Size { get; set; }
}
