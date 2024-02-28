using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;

public class PlayerGeneralProfileDto : IMapFrom<Player>
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
        profile.CreateMap<Player, PlayerGeneralProfileDto>()
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName))
                                                   .ForMember(p => p.Biography, d => d.MapFrom(z => z.GeneralProfile.Biography))
                                                   .ForMember(p => p.Birthday, d => d.MapFrom(z => z.GeneralProfile.Birthday))
                                                   .ForMember(p => p.City, d => d.MapFrom(z => z.GeneralProfile.City))
                                                   .ForMember(p => p.FreePlay, d => d.MapFrom(z => z.GeneralProfile.FreePlay));

        profile.CreateMap<PlayerGeneralProfileModel, PlayerGeneralProfileDto>();
    }
}


