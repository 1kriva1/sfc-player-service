using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersStatsBySkillRangeLimitDto :
    RangeLimitDto<short?>,
    IMapFrom<GetPlayersStatsBySkillRangeLimitModel>
{
    public int Skill { get; set; }
}
