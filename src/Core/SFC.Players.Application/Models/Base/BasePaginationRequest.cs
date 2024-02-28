using SFC.Players.Application.Models.Common;
using SFC.Players.Application.Models.Common.Pagination;

namespace SFC.Players.Application.Models.Base;
public class BasePaginationRequest<T> where T : class
{
    public PaginationModel Pagination { get; set; } = default!;

    public IEnumerable<SortingModel> Sorting { get; set; } = default!;

    public T Filter { get; set; } = default!;
}
