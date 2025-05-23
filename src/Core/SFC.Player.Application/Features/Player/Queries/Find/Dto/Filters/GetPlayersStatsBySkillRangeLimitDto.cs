using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersStatsBySkillRangeLimitDto : RangeLimitDto<short?>
{
    public int Skill { get; set; }
}
