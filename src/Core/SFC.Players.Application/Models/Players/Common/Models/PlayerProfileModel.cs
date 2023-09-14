using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common.Models;
public class PlayerProfileModel : IMapFrom<Player>
{
    public PlayerGeneralProfileModel General { get; set; } = null!;

    public PlayerFootballProfileDto Football { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerProfileModel>()
                                                   .ForMember(p => p.General, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Football, d => d.MapFrom(z => z.FootballProfile));
}