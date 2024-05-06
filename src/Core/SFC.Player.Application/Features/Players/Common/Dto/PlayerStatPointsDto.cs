using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Players.Common;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Application.Features.Players.Common.Dto;

public record PlayerStatPointsDto : IMapFrom<PlayerStatPoints>
{
    public int Available { get; set; }

    public int Used { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerStatPoints, PlayerStatPointsDto>();
        profile.CreateMap<PlayerStatPointsModel, PlayerStatPointsDto>();
    }
}
