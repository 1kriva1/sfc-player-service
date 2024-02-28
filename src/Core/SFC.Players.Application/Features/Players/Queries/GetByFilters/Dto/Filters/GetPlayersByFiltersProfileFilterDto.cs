using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersProfileFilterDto : IMapFrom<GetPlayersByFiltersProfileFilterModel>
{
    public GetPlayersByFiltersGeneralProfileFilterDto General { get; set; } = default!;

    public GetPlayersByFiltersFootballProfileFilterDto Football { get; set; } = default!;
}
