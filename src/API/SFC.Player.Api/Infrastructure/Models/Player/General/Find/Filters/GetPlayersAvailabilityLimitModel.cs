using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;

/// <summary>
/// Get players **availability filter** model.
/// </summary>
public class GetPlayersAvailabilityLimitModel :
    RangeLimitModel<TimeSpan?>,
    IMapTo<GetPlayersAvailabilityLimitDto>
{
    /// <summary>
    /// Day of week.
    /// </summary>
    public IEnumerable<int> Days { get; set; } = default!;
}
