using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerDto : IMapFrom<PlayerEntity>
{
    public long Id { get; set; }

    public PlayerProfileDto Profile { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z));
}
