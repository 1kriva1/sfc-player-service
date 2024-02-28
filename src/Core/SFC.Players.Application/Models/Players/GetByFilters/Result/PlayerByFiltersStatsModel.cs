using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
using SFC.Players.Application.Models.Players.Common;

namespace SFC.Players.Application.Models.Players.GetByFilters.Result;
public class PlayerByFiltersStatsModel : IMapFrom<PlayerByFiltersStatsDto>
{
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
