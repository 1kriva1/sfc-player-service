using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;

namespace SFC.Player.Application.Models.Players.Find.Result;

/// <summary>
/// Player model for get players request.
/// </summary>
public class PlayerModel: IMapFrom<PlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Player's profile model.
    /// </summary>
    public PlayerProfileModel Profile { get; set; } = null!;

    /// <summary>
    /// Player's stats model.
    /// </summary>
    public PlayerStatsModel Stats { get; set; } = null!;
}
