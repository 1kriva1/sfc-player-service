using SFC.Players.Application.Models.Common;

namespace SFC.Players.Application.Models.Players.GetByFilters.Filters;
public class GetPlayersByFiltersStatsBySkillRangeLimitModel : RangeLimitModel<short?>
{
    public int Skill { get; set; }
}
