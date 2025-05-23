using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Common;

/// <summary>
/// Player model.
/// </summary>
public class PlayerModel : BasePlayerModel, IMapFrom<PlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }
}
