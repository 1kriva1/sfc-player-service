using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;
public class GetPlayersByFiltersGeneralProfileFilterModel
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public IEnumerable<string> Tags { get; set; } = default!;

    public RangeLimitModel<short?> Years { get; set; } = default!;

    public GetPlayersByFiltersAvailabilityLimitModel Availability { get; set; } = default!;

    public bool? FreePlay { get; set; }

    public bool? HasPhoto { get; set; }
}
