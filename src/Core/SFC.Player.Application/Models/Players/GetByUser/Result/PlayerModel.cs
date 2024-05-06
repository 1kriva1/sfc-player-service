using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Models.Players.GetByUser.Result;

/// <summary>
/// Player model for get by user request.
/// </summary>
public class PlayerModel : IMapFrom<PlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Player profile model.
    /// </summary>
    public PlayerProfileModel Profile { get; set; } = null!;
}
