using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersFilterDto : IMapFrom<GetPlayersFilterModel>
{
    public GetPlayersProfileFilterDto Profile { get; set; } = default!;

    public GetPlayersStatsFilterDto Stats { get; set; } = default!;
}
