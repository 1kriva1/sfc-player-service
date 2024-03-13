using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersGeneralProfileFilterDto : IMapFrom<GetPlayersByFiltersGeneralProfileFilterModel>
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();

    public RangeLimitDto<short?> Years { get; set; } = default!;

    public GetPlayersByFiltersAvailabilityLimitDto Availability { get; set; } = default!;

    public bool? FreePlay { get; set; }

    public bool? HasPhoto { get; set; }
}
