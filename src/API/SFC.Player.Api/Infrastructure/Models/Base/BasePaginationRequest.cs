using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Api.Infrastructure.Models.Pagination;

namespace SFC.Player.Api.Infrastructure.Models.Base;

/// <summary>
/// **Base** pagination request.
/// </summary>
/// <typeparam name="T">Any type of filter model.</typeparam>
public class BasePaginationRequest<T> where T : class
{
    /// <summary>
    /// Pagination model.
    /// </summary>
    public PaginationModel Pagination { get; set; } = default!;

    /// <summary>
    /// Sorting model.
    /// </summary>
    public IEnumerable<SortingModel> Sorting { get; set; } = default!;

    /// <summary>
    /// Generic filter model.
    /// </summary>
    public T Filter { get; set; } = default!;
}
