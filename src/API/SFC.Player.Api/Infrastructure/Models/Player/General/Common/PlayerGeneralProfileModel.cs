﻿using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player's **general** profile model.
/// </summary>
public class PlayerGeneralProfileModel : IMapFromReverse<PlayerGeneralProfileDto>
{
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Photo/avatar.
    /// </summary>
    public string? Photo { get; set; }

    /// <summary>
    /// Biography.
    /// </summary>
    public string? Biography { get; set; }

    /// <summary>
    /// Date of birth.
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// **City** where player will play football.
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    ///  Describe if player can **pay** for football matches.
    /// </summary>
    public bool FreePlay { get; set; }

    /// <summary>
    /// Player's **tags**.
    /// </summary>
    public IEnumerable<string> Tags { get; set; } = [];

    /// <summary>
    /// Player's **availability** model.
    /// </summary>
    public PlayerAvailabilityModel Availability { get; set; } = null!;
}
