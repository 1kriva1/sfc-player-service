namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;
public class GetPlayersByFiltersProfileFilterModel
{
    public GetPlayersByFiltersGeneralProfileFilterModel General { get; set; } = default!;

    public GetPlayersByFiltersFootballProfileFilterModel Football { get; set; } = default!;
}
