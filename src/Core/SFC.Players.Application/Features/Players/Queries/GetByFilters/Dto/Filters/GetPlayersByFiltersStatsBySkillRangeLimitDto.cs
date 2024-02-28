using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
public class GetPlayersByFiltersStatsBySkillRangeLimitDto :
    RangeLimitDto<short?>,
    IMapFrom<GetPlayersByFiltersStatsBySkillRangeLimitModel>
{
    public int Skill { get; set; }
}
