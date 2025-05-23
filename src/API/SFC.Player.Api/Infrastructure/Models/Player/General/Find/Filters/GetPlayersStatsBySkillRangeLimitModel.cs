using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;
/// <summary>
/// Range limit by **stat skill**.
/// </summary>
public class GetPlayersStatsBySkillRangeLimitModel :
    RangeLimitModel<short?>,
    IMapTo<GetPlayersStatsBySkillRangeLimitDto>
{
    /// <summary>
    /// Stat skill unique identifier.
    /// </summary>
    public int Skill { get; set; }
}
