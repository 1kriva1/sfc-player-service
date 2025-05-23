using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;

/// <summary>
/// Player's **football** profile model for get by user request.
/// </summary>
public class GetByUserPlayerFootballProfileModel : IMapFrom<PlayerFootballProfileDto>
{
    /// <summary>
    /// Position on field.
    /// </summary>
    public int? Position { get; set; }
}
