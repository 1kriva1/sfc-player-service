using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

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
