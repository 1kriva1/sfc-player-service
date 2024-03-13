using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;
public class GetPlayersByFiltersAvailabilityLimitModel : RangeLimitModel<TimeSpan?>
{
    public IEnumerable<int> Days { get; set; } = default!;
}
