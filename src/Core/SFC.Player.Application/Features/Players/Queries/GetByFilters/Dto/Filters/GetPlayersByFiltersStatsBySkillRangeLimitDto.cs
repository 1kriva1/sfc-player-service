using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersStatsBySkillRangeLimitDto :
    RangeLimitDto<short?>,
    IMapFrom<GetPlayersByFiltersStatsBySkillRangeLimitModel>
{
    public int Skill { get; set; }
}
