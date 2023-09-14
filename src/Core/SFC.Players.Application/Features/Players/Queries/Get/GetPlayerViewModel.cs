using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common.Models;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public record GetPlayerViewModel : IMapFrom<Player>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, GetPlayerViewModel>()
                                                   .ForMember(p => p.Player, d => d.MapFrom(z => z));
}
