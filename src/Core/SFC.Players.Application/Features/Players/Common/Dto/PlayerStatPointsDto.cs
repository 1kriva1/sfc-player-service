using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;

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
