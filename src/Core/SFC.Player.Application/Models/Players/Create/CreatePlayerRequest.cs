namespace SFC.Player.Application.Models.Players.Create;

/// <summary>
/// **Create** player request.
/// </summary>
public class CreatePlayerRequest
{
    /// <summary>
    /// Player model.
    /// </summary>
    public CreatePlayerModel Player { get; set; } = null!;
}
