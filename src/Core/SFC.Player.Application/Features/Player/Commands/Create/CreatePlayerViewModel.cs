using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Commands.Create;

public record CreatePlayerViewModel: IMapFrom<PlayerEntity>
{
    public PlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, CreatePlayerViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
