using AutoMapper;

using SFC.Player.Application.Common.Dto.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;

namespace SFC.Player.Application.Features.Player.Common.Dto;
public class BasePlayerDto : AuditableDto, IMapToReverse<PlayerEntity>
{
    public PlayerProfileDto Profile { get; set; } = null!;

    public PlayerStatsDto Stats { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BasePlayerDto, PlayerEntity>()
                .ForMember(p => p.GeneralProfile, d => d.MapFrom(z => z.Profile.General))
                .ForMember(p => p.FootballProfile, d => d.MapFrom(z => z.Profile.Football))
                .ForMember(p => p.Availability, d => d.MapFrom(z => z.Profile.General.Availability))
                .ForMember(p => p.Points, d => d.MapFrom(z => z.Stats.Points))
                .ForMember(p => p.Photo, d => d.MapFrom(z => z.Profile.General.Photo))
                .ForMember(p => p.Tags, d => d.MapFrom(z => z.Profile.General.Tags))
                .ForMember(p => p.Stats, d => d.MapFrom(z => z.Stats.Values))
                .ForMember(p => p.CreatedDate, d => d.Ignore())
                .ForMember(p => p.CreatedBy, d => d.Ignore())
                .ForMember(p => p.LastModifiedDate, d => d.Ignore())
                .ForMember(p => p.LastModifiedBy, d => d.Ignore())
                .ForMember(p => p.Id, d => d.Ignore())
                .ForMember(p => p.DomainEvents, d => d.Ignore())
                .ReverseMap();
    }
}