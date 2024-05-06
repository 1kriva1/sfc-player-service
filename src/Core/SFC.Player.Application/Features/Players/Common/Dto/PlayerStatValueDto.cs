using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Players.Common;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Application.Features.Players.Common.Dto;
public record PlayerStatValueDto : IMapFrom<PlayerStat>
{
    public int Type { get; set; } = default!;

    public byte Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerStat, PlayerStatValueDto>();
        profile.CreateMap<PlayerStatValueModel, PlayerStatValueDto>();
    }
}