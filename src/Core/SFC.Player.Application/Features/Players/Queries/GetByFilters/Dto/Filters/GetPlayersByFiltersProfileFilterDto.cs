using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersProfileFilterDto : IMapFrom<GetPlayersByFiltersProfileFilterModel>
{
    public GetPlayersByFiltersGeneralProfileFilterDto General { get; set; } = default!;

    public GetPlayersByFiltersFootballProfileFilterDto Football { get; set; } = default!;
}
