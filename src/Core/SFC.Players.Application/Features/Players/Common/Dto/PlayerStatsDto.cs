using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;

public class PlayerStatsDto : IMapFrom<Player>
{
    public PlayerStatPointsDto Points { get; set; } = null!;

    public IEnumerable<PlayerStatValueDto> Values { get; set; } = new List<PlayerStatValueDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, PlayerStatsDto>()
            .ForMember(p => p.Points, d => d.MapFrom(z => z.Points))
            .ForMember(p => p.Values, d => d.MapFrom(z => z.Stats));

        profile.CreateMap<PlayerStatsModel, PlayerStatsDto>();
    }
}