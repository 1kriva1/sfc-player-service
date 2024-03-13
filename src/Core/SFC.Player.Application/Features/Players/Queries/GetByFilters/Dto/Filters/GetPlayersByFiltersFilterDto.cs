using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersFilterDto : IMapFrom<GetPlayersByFiltersFilterModel>
{
    public GetPlayersByFiltersProfileFilterDto Profile { get; set; } = default!;

    public GetPlayersByFiltersStatsFilterDto Stats { get; set; } = default!;
}
