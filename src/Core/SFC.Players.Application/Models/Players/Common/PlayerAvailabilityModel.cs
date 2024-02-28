using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Models.Common;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerAvailabilityModel : RangeLimitModel<TimeSpan?>, IMapFrom<PlayerAvailabilityDto>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();
}
