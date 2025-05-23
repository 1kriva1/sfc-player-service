using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;

/// <summary>
/// Player model for get by user request.
/// </summary>
public class GetByUserPlayerModel : IMapFrom<PlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Player profile model.
    /// </summary>
    public GetByUserPlayerProfileModel Profile { get; set; } = null!;
}
