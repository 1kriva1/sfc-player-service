using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Features.Common.Dto;

namespace SFC.Players.Application.Features.Common.Base;
public abstract class BasePaginationRequest<TResponse, TFilter> : Request<TResponse>
{
    public PaginationDto Pagination { get; set; } = default!;

    public IEnumerable<SortingDto> Sorting { get; set; } = default!;

    public TFilter Filter { get; set; } = default!;

    public string Route { get; set; } = default!;

    public string QueryString { get; set; } = default!;

    public BasePaginationRequest<TResponse, TFilter> SetRoute(string route)
    {
        Route = route;
        return this;
    }

    public BasePaginationRequest<TResponse, TFilter> SetQueryString(string queryString)
    {
        QueryString = queryString;
        return this;
    }
}
