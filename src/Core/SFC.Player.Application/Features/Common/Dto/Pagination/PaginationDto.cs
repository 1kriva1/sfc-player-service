using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Common.Pagination;

namespace SFC.Player.Application.Features.Common.Dto.Pagination;
public class PaginationDto : IMapFrom<PaginationModel>
{
    public int Page { get; set; }

    public int Size { get; set; }
}
