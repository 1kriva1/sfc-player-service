﻿using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;
using SFC.Player.Application.Models.Players.Common;

namespace SFC.Player.Application.Models.Players.Find.Result;

/// <summary>
/// Player's **general** profile model for get players request.
/// </summary>
public class PlayerGeneralProfileModel : IMapFrom<PlayerGeneralProfileDto>
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
    public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Player's **availability** model.
    /// </summary>
    public PlayerAvailabilityModel Availability { get; set; } = null!;
}
