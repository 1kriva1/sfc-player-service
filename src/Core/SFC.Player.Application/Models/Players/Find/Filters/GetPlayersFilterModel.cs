namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players filter model.
/// </summary>
public class GetPlayersFilterModel
{
    /// <summary>
    /// Profile filter model.
    /// </summary>
    public GetPlayersProfileFilterModel Profile { get; set; } = default!;

    /// <summary>
    /// Stats filter model.
    /// </summary>
    public GetPlayersStatsFilterModel Stats { get; set; } = default!;
}
