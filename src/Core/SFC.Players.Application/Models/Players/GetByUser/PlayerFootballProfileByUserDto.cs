using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;
using AutoMapper;

namespace SFC.Players.Application.Models.Players.GetByUser;
public class PlayerFootballProfileByUserDto : IMapFrom<PlayerFootballProfile>
{
    public int? Position { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerFootballProfile, PlayerFootballProfileByUserDto>()
                                                   .ForMember(p => p.Position, d => d.MapFrom(z => z.PositionId));
}