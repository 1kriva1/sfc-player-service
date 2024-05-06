namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players **profile filter** model.
/// </summary>
public class GetPlayersProfileFilterModel
{
    /// <summary>
    /// General profile.
    /// </summary>
    public GetPlayersGeneralProfileFilterModel General { get; set; } = default!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public GetPlayersFootballProfileFilterModel Football { get; set; } = default!;
}
