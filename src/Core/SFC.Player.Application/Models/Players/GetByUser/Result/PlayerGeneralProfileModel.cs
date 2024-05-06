using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Features.Players.Queries.GetByUser.Dto;

using PlayerGeneralProfileDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerGeneralProfileDto;

namespace SFC.Player.Application.Models.Players.GetByUser.Result;

/// <summary>
/// Player's **general** profile model for get by user request.
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
}
