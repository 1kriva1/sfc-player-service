using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common;

public record PlayerAvailabilityDto : IMapFromReverse<PlayerAvailability>
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public IEnumerable<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();
}
