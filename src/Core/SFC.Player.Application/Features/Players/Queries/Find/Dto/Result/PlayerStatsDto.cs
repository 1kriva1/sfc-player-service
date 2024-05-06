using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;
public class PlayerStatsDto : IMapFrom<PlayerEntity>
{
    public IEnumerable<PlayerStatValueDto> Values { get; set; } = new List<PlayerStatValueDto>();

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerStatsDto>()
                                                   .ForMember(p => p.Values, d => d.MapFrom(z => z.Stats));
}
