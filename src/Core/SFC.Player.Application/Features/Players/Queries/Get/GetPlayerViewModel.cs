using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Queries.Get;

public record GetPlayerViewModel : IMapFrom<PlayerEntity>
{
    public PlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, GetPlayerViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
