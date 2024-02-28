namespace SFC.Players.Application.Models.Players.GetByFilters.Filters;
public class GetPlayersByFiltersProfileFilterModel
{
    public GetPlayersByFiltersGeneralProfileFilterModel General { get; set; } = default!;

    public GetPlayersByFiltersFootballProfileFilterModel Football { get; set; } = default!;
}
