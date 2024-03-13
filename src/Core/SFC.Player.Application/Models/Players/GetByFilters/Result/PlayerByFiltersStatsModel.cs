using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
using SFC.Player.Application.Features.Player.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Result;
public class PlayerByFiltersStatsModel : IMapFrom<PlayerByFiltersStatsDto>
{
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
