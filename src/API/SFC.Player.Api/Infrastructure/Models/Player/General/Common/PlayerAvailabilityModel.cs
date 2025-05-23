using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player's **availability** model (when player is available to play).
/// </summary>
public class PlayerAvailabilityModel :
    RangeLimitModel<TimeSpan?>,
    IMapFromReverse<PlayerAvailabilityDto>
{
    /// <summary>
    /// Days of week.
    /// </summary>
    public IEnumerable<DayOfWeek> Days { get; set; } = [];
}
