namespace SFC.Players.Application.Models.Players.GetByFilters.Filters;

public class GetPlayersByFiltersFilterModel
{
    public GetPlayersByFiltersProfileFilterModel Profile { get; set; } = default!;

    public GetPlayersByFiltersStatsFilterModel Stats { get; set; } = default!;
}
