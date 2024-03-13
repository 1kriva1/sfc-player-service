using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
public class PlayerByFiltersDto : IMapFrom<PlayerEntity>
{
    public long Id { get; set; }

    public PlayerByFiltersProfileDto Profile { get; set; } = null!;

    public PlayerByFiltersStatsDto Stats { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerByFiltersDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Stats, d => d.MapFrom(z => z));
}
