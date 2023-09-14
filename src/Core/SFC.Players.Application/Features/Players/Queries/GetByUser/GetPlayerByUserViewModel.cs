using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.GetByUser;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public record GetPlayerByUserViewModel : IMapFrom<Player>
{
    public PlayerByUserDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, GetPlayerByUserViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
