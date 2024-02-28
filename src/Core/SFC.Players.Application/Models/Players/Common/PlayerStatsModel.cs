using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerStatsModel : IMapFrom<PlayerStatsDto>
{
    public PlayerStatPointsModel Points { get; set; } = null!;

    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
