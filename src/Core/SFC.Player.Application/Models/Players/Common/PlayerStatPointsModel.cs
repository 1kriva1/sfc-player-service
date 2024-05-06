using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// Player' **stats points** model.
/// </summary>
public class PlayerStatPointsModel : IMapFrom<PlayerStatPointsDto>
{
    /// <summary>
    /// Available points.
    /// </summary>
    public int Available { get; set; }

    /// <summary>
    /// Used points.
    /// </summary>
    public int Used { get; set; }
}
