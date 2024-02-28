using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;
public class PlayerDto : BasePlayerDto, IMapFrom<Player>
{
    public long Id { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Stats, d => d.MapFrom(z => z));
}
