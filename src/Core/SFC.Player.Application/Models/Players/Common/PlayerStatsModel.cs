using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// Player stats model.
/// </summary>
public class PlayerStatsModel : IMapFrom<PlayerStatsDto>
{
    /// <summary>
    /// Stats points.
    /// </summary>
    public PlayerStatPointsModel Points { get; set; } = null!;

    /// <summary>
    /// Stats.
    /// </summary>
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
