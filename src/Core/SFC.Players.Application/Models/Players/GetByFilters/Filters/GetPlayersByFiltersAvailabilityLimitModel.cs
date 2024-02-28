using SFC.Players.Application.Models.Common;

namespace SFC.Players.Application.Models.Players.GetByFilters.Filters;
public class GetPlayersByFiltersAvailabilityLimitModel : RangeLimitModel<TimeSpan?>
{
    public IEnumerable<int> Days { get; set; } = default!;
}
