﻿using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Models.Players.GetByUser.Result;

/// <summary>
/// Player **profile** model for get by user request.
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
