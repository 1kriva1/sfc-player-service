using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersAvailabilityLimitDto :
    RangeLimitDto<TimeSpan?>,
    IMapFrom<GetPlayersByFiltersAvailabilityLimitModel>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = Array.Empty<DayOfWeek>();
}
