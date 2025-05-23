using SFC.Player.Application.Features.Common.Models.Find.Filters;
using SFC.Player.Application.Features.Common.Models.Find.Paging;
using SFC.Player.Application.Features.Common.Models.Find.Sorting;

namespace SFC.Player.Application.Features.Common.Models.Find;
public class FindParameters<T>
{
    public Filters<T>? Filters { get; set; }

    public Sortings<T>? Sorting { get; set; }

    public required Pagination Pagination { get; set; }
}
