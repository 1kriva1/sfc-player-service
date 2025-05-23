using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Queries.Get;

public record GetPlayerViewModel : IMapFrom<PlayerEntity>
{
    public PlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, GetPlayerViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
