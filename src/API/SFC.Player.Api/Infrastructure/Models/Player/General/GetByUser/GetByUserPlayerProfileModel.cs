using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;

/// <summary>
/// Player **profile** model for get by user request.
/// </summary>
public class GetByUserPlayerProfileModel : IMapFrom<PlayerProfileDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public GetByUserPlayerGeneralProfileModel General { get; set; } = null!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public GetByUserPlayerFootballProfileModel Football { get; set; } = null!;
}
