using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;
public class PlayerDto : BasePlayerDto, IMapFrom<PlayerEntity>
{
    public long Id { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Stats, d => d.MapFrom(z => z));
}
