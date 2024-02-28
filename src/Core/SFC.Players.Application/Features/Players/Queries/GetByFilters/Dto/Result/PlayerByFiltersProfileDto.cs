using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
public class PlayerByFiltersProfileDto : IMapFrom<Player>
{
    public PlayerByFiltersGeneralProfileDto General { get; set; } = null!;

    public PlayerByFiltersFootballProfileDto Football { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerByFiltersProfileDto>()
                                                   .ForMember(p => p.General, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Football, d => d.MapFrom(z => z.FootballProfile));
}
