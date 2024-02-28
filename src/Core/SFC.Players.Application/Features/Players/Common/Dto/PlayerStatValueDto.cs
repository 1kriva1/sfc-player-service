using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;
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