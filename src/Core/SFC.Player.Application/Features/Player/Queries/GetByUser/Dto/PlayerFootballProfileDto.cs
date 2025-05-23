using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerFootballProfileDto : IMapFrom<PlayerFootballProfile>
{
    public int? Position { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerFootballProfile, PlayerFootballProfileDto>()
                                                   .ForMember(p => p.Position, d => d.MapFrom(z => z.PositionId));
}