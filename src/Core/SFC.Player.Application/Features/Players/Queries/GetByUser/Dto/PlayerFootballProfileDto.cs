using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;
using AutoMapper;

namespace SFC.Player.Application.Features.Players.Queries.GetByUser.Dto;
public class PlayerFootballProfileDto : IMapFrom<PlayerFootballProfile>
{
    public int? Position { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerFootballProfile, PlayerFootballProfileDto>()
                                                   .ForMember(p => p.Position, d => d.MapFrom(z => z.PositionId));
}