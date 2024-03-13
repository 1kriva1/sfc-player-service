using SFC.Player.Application.Models.Common;
using SFC.Player.Application.Models.Common.Pagination;

namespace SFC.Player.Application.Models.Base;
public class BasePaginationRequest<T> where T : class
{
    public PaginationModel Pagination { get; set; } = default!;

    public IEnumerable<SortingModel> Sorting { get; set; } = default!;

    public T Filter { get; set; } = default!;
}
