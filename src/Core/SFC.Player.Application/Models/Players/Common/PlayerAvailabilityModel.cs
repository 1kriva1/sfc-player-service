using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerAvailabilityModel : RangeLimitModel<TimeSpan?>, IMapFrom<PlayerAvailabilityDto>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();
}
