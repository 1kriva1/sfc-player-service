using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common.Models;
public class PlayerModel : BasePlayerModel, IMapFrom<Player>
{
    public long Id { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerModel>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Stats, d => d.MapFrom(z => z));
}
