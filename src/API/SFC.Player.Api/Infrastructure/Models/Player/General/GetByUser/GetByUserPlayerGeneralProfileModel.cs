using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

using PlayerGeneralProfileDto = SFC.Player.Application.Features.Player.Queries.GetByUser.Dto.PlayerGeneralProfileDto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;

/// <summary>
/// Player's **general** profile model for get by user request.
/// </summary>
public class GetByUserPlayerGeneralProfileModel : IMapFrom<PlayerGeneralProfileDto>
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
