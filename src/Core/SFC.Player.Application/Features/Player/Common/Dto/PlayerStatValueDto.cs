using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;
public record PlayerStatValueDto : IMapFromReverse<PlayerStat>
{
    public int Type { get; set; } = default!;

    public byte Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerStatValueDto, PlayerStat>()
            .ForMember(p => p.DomainEvents, d => d.Ignore())
            .ForMember(p => p.Id, d => d.Ignore())
            .ForMember(p => p.Player, d => d.Ignore())
            .ForMember(p => p.Type, d => d.Ignore())
            .ForMember(p => p.TypeId, d => d.MapFrom(e=>e.Type))            
            .ReverseMap();
    }
}