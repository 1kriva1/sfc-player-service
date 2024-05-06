using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players **stats filter** model.
/// </summary>
public class GetPlayersStatsFilterModel
{
    /// <summary>
    /// Filter by total rating.
    /// </summary>
    public RangeLimitModel<short?> Total { get; set; } = default!;

    /// <summary>
    /// Filter by physical stats rating.
    /// </summary>
    public GetPlayersStatsBySkillRangeLimitModel Physical { get; set; } = default!;

    /// <summary>
    /// Filter by mental stats rating.
    /// </summary>
    public GetPlayersStatsBySkillRangeLimitModel Mental { get; set; } = default!;

    /// <summary>
    /// Filter by skill stats rating.
    /// </summary>
    public GetPlayersStatsBySkillRangeLimitModel Skill { get; set; } = default!;

    /// <summary>
    /// Filter by rating.
    /// </summary>
    public int? Raiting { get; set; }
}
