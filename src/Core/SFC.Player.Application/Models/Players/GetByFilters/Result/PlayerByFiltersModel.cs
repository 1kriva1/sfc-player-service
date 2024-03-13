using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;

namespace SFC.Player.Application.Features.Player.GetByFilters.Result;
public class PlayerByFiltersModel: IMapFrom<PlayerByFiltersDto>
{
    public long Id { get; set; }

    public PlayerByFiltersProfileModel Profile { get; set; } = null!;

    public PlayerByFiltersStatsModel Stats { get; set; } = null!;
}
