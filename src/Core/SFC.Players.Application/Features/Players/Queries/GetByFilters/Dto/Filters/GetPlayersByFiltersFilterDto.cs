using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersFilterDto : IMapFrom<GetPlayersByFiltersFilterModel>
{
    public GetPlayersByFiltersProfileFilterDto Profile { get; set; } = default!;

    public GetPlayersByFiltersStatsFilterDto Stats { get; set; } = default!;
}
