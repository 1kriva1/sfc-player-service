using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;

namespace SFC.Players.Application.Models.Players.GetByFilters.Result;
public class PlayerByFiltersModel: IMapFrom<PlayerByFiltersDto>
{
    public long Id { get; set; }

    public PlayerByFiltersProfileModel Profile { get; set; } = null!;

    public PlayerByFiltersStatsModel Stats { get; set; } = null!;
}
