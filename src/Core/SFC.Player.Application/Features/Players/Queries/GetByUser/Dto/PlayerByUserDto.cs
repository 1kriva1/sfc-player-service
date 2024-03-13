using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerByUserDto : IMapFrom<PlayerEntity>
{
    public long Id { get; set; }

    public PlayerProfileByUserDto Profile { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerByUserDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z));
}
