using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
public class PlayerByFiltersProfileDto : IMapFrom<PlayerEntity>
{
    public PlayerByFiltersGeneralProfileDto General { get; set; } = null!;

    public PlayerByFiltersFootballProfileDto Football { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerByFiltersProfileDto>()
                                                   .ForMember(p => p.General, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Football, d => d.MapFrom(z => z.FootballProfile));
}
