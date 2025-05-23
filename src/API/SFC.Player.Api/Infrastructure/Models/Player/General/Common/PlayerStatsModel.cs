using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player stats model.
/// </summary>
public class PlayerStatsModel : IMapFromReverse<PlayerStatsDto>
{
    /// <summary>
    /// Stats points.
    /// </summary>
    public PlayerStatPointsModel Points { get; set; } = null!;

    /// <summary>
    /// Stats.
    /// </summary>
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = [];
}
