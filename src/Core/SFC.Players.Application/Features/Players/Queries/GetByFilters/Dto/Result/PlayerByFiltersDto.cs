using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
public class PlayerByFiltersDto : IMapFrom<Player>
{
    public long Id { get; set; }

    public PlayerByFiltersProfileDto Profile { get; set; } = null!;

    public PlayerByFiltersStatsDto Stats { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerByFiltersDto>()
                                                   .ForMember(p => p.Profile, d => d.MapFrom(z => z))
                                                   .ForMember(p => p.Stats, d => d.MapFrom(z => z));
}
