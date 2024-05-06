using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// Player **profile** model.
/// </summary>
public class PlayerProfileModel : IMapFrom<PlayerProfileDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public PlayerGeneralProfileModel General { get; set; } = null!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public PlayerFootballProfileModel Football { get; set; } = null!;
}