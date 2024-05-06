using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players **general profile filter** model.
/// </summary>
public class GetPlayersGeneralProfileFilterModel
{
    /// <summary>
    /// Name (first and last).
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// **City** where player will play football.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Player's **tags**.
    /// </summary>
    public IEnumerable<string> Tags { get; set; } = default!;

    /// <summary>
    /// Range limit for players age.
    /// </summary>
    public RangeLimitModel<short?> Years { get; set; } = default!;

    /// <summary>
    /// Player's **availability** model.
    /// </summary>
    public GetPlayersAvailabilityLimitModel Availability { get; set; } = default!;

    /// <summary>
    ///  Describe if player can **pay** for football matches.
    /// </summary>
    public bool? FreePlay { get; set; }

    /// <summary>
    ///  Describe if player must have uploaded photo.
    /// </summary>
    public bool? HasPhoto { get; set; }
}
