using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player' **stats points** model.
/// </summary>
public class PlayerStatPointsModel : IMapFromReverse<PlayerStatPointsDto>
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
