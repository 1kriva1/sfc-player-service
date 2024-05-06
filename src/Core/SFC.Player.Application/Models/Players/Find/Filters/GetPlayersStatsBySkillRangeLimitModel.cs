using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Models.Players.Find.Filters;
/// <summary>
/// Range limit by **stat skill**.
/// </summary>
public class GetPlayersStatsBySkillRangeLimitModel : RangeLimitModel<short?>
{
    /// <summary>
    /// Stat skill unique identifier.
    /// </summary>
    public int Skill { get; set; }
}
