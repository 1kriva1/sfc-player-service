using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player stat value model.
/// </summary>
public class PlayerStatValueModel : IMapFromReverse<PlayerStatValueDto>
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
