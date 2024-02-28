using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
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
