namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// **Base** player model.
/// </summary>
public class BasePlayerModel
{
    /// <summary>
    /// Player's profile model.
    /// </summary>
    public PlayerProfileModel Profile { get; set; } = null!;

    /// <summary>
    /// Player's stats model.
    /// </summary>
    public PlayerStatsModel Stats { get; set; } = null!;
}
