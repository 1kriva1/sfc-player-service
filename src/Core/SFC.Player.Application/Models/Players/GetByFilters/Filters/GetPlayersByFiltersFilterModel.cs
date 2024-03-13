namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;

public class GetPlayersByFiltersFilterModel
{
    public GetPlayersByFiltersProfileFilterModel Profile { get; set; } = default!;

    public GetPlayersByFiltersStatsFilterModel Stats { get; set; } = default!;
}
