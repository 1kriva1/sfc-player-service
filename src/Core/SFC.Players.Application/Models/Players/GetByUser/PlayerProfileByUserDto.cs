using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.GetByUser;
public class PlayerProfileByUserDto : IMapFrom<Player>
{
    public PlayerGeneralProfileByUserDto General { get; set; } = null!;

    public PlayerFootballProfileByUserDto Football { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerProfileByUserDto>()
                                                   .ForMember(p => p.General, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Football, d => d.MapFrom(z => z.FootballProfile));
}
