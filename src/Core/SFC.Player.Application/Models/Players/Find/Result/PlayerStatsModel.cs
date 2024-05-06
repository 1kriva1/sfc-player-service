using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;
using SFC.Player.Application.Models.Players.Common;

namespace SFC.Player.Application.Models.Players.Find.Result;

/// <summary>
/// Player stats model for get players request.
/// </summary>
public class PlayerStatsModel : IMapFrom<PlayerStatsDto>
{
    /// <summary>
    /// Stats.
    /// </summary>
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = Array.Empty<PlayerStatValueModel>();
}
