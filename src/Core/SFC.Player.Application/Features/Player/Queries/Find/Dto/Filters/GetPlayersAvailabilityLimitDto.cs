using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersAvailabilityLimitDto : RangeLimitDto<TimeSpan?>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = [];
}
