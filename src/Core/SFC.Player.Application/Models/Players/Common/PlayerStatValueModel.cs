using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// Player stat value model.
/// </summary>
public class PlayerStatValueModel : IMapFrom<PlayerStatValueDto>
{
    /// <summary>
    /// Type of stat
    /// </summary>
    public int Type { get; set; } = default!;

    /// <summary>
    /// Stat value.
    /// </summary>
    public byte Value { get; set; }
}
