using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public class PlayerGeneralProfileDto : IMapFrom<PlayerEntity>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public PlayerPhotoDto? Photo { get; set; }

    public string? Biography { get; set; }

    public DateTime? Birthday { get; set; }

    public string City { get; set; } = null!;

    public bool FreePlay { get; set; }

    public IEnumerable<string> Tags { get; set; } = new List<string>();

    public PlayerAvailabilityDto Availability { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerEntity, PlayerGeneralProfileDto>()
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName))
                                                   .ForMember(p => p.Biography, d => d.MapFrom(z => z.GeneralProfile.Biography))
                                                   .ForMember(p => p.Birthday, d => d.MapFrom(z => z.GeneralProfile.Birthday))
                                                   .ForMember(p => p.City, d => d.MapFrom(z => z.GeneralProfile.City))
                                                   .ForMember(p => p.FreePlay, d => d.MapFrom(z => z.GeneralProfile.FreePlay));

        profile.CreateMap<PlayerGeneralProfileModel, PlayerGeneralProfileDto>();
    }
}


