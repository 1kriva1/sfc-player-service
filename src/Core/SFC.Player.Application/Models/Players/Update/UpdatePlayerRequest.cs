namespace SFC.Player.Application.Models.Players.Update;

/// <summary>
/// **Update** player request.
/// </summary>
public class UpdatePlayerRequest
{
    /// <summary>
    /// Player model.
    /// </summary>
    public UpdatePlayerModel Player { get; set; } = null!;
}
