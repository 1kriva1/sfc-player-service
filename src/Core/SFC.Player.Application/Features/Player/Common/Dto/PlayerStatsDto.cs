using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public class PlayerStatsDto : IMapFromReverse<PlayerEntity>
{
    public PlayerStatPointsDto Points { get; set; } = null!;

    public IEnumerable<PlayerStatValueDto> Values { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerEntity, PlayerStatsDto>()
            .ForMember(p => p.Values, d => d.MapFrom(z => z.Stats))
            .ReverseMap();
    }
}