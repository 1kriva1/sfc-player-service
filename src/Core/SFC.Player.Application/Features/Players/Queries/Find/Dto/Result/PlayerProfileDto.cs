﻿using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;
public class PlayerProfileDto : IMapFrom<PlayerEntity>
{
    public PlayerGeneralProfileDto General { get; set; } = null!;

    public PlayerFootballProfileDto Football { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerProfileDto>()
                                                   .ForMember(p => p.General, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Football, d => d.MapFrom(z => z.FootballProfile));
}
