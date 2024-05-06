using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Models.Players.GetByUser.Result;

/// <summary>
/// Player's **football** profile model for get by user request.
/// </summary>
public class PlayerFootballProfileModel : IMapFrom<PlayerFootballProfileDto>
{
    /// <summary>
    /// Position on field.
    /// </summary>
    public int? Position { get; set; }
}
