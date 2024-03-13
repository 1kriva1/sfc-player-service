using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;
using AutoMapper;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerFootballProfileByUserDto : IMapFrom<PlayerFootballProfile>
{
    public int? Position { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerFootballProfile, PlayerFootballProfileByUserDto>()
                                                   .ForMember(p => p.Position, d => d.MapFrom(z => z.PositionId));
}