using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;

/// <summary>
/// Get players **stats filter** model.
/// </summary>
public class GetPlayersStatsFilterModel : IMapTo<GetPlayersStatsFilterDto>
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
