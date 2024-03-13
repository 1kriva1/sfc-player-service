using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerStatsModel : IMapFrom<PlayerStatsDto>
{
    public PlayerStatPointsModel Points { get; set; } = null!;

    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
