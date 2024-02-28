using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;
public class PlayerByUserDto : IMapFrom<Player>
{
    public long Id { get; set; }

    public PlayerProfileByUserDto Profile { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerByUserDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z));
}
