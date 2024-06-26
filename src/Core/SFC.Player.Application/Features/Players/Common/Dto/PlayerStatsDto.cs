﻿using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Players.Common;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Common.Dto;

public class PlayerStatsDto : IMapFrom<PlayerEntity>
{
    public PlayerStatPointsDto Points { get; set; } = null!;

    public IEnumerable<PlayerStatValueDto> Values { get; set; } = new List<PlayerStatValueDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerEntity, PlayerStatsDto>()
            .ForMember(p => p.Points, d => d.MapFrom(z => z.Points))
            .ForMember(p => p.Values, d => d.MapFrom(z => z.Stats));

        profile.CreateMap<PlayerStatsModel, PlayerStatsDto>();
    }
}