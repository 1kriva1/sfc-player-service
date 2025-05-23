using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player **profile** model.
/// </summary>
public class PlayerProfileModel : IMapFromReverse<PlayerProfileDto>
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