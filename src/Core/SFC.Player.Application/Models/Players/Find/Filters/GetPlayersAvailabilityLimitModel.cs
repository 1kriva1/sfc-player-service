using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players **availability filter** model.
/// </summary>
public class GetPlayersAvailabilityLimitModel : RangeLimitModel<TimeSpan?>
{
    /// <summary>
    /// Day of week.
    /// </summary>
    public IEnumerable<int> Days { get; set; } = default!;
}
