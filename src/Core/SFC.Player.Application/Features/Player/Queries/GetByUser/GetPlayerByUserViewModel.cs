using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser;

public record GetPlayerByUserViewModel : IMapFrom<PlayerEntity>
{
    public PlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, GetPlayerByUserViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
