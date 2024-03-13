using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Filters;
public class GetPlayersByFiltersStatsBySkillRangeLimitModel : RangeLimitModel<short?>
{
    public int Skill { get; set; }
}
