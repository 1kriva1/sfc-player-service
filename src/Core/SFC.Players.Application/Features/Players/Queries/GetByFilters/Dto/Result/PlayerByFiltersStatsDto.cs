using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
public class PlayerByFiltersStatsDto : IMapFrom<Player>
{
    public IEnumerable<PlayerStatValueDto> Values { get; set; } = new List<PlayerStatValueDto>();

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerByFiltersStatsDto>()
                                                   .ForMember(p => p.Values, d => d.MapFrom(z => z.Stats));
}
