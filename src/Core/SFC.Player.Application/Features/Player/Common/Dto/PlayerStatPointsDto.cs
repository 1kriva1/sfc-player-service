using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public record PlayerStatPointsDto : IMapFromReverse<PlayerStatPoints>
{
    public int Available { get; set; }

    public int Used { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerStatPointsDto, PlayerStatPoints>()
               .ReverseMap()
               .IgnoreAllNonExisting();
    }
}
