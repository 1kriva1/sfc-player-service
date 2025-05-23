using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Common.Base;
public abstract class BasePaginationRequest<TResponse, TFilter> : Request<TResponse>
{
    public PaginationDto Pagination { get; set; } = default!;

    public IEnumerable<SortingDto> Sorting { get; set; } = default!;

    public TFilter Filter { get; set; } = default!;
}
