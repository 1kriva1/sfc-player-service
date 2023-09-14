using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common.Models;
public class PlayerGeneralProfileModel : IMapFrom<Player>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Biography { get; set; }

    public DateTime? Birthday { get; set; }

    public string City { get; set; } = null!;

    public bool FreePlay { get; set; }

    public IEnumerable<string> Tags { get; set; } = new List<string>();

    public PlayerAvailabilityDto Availability { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerGeneralProfileModel>()
                                                   .ForMember(p => p.Photo, d => d.MapFrom(z => z.Photo))
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName))
                                                   .ForMember(p => p.FreePlay, d => d.MapFrom(z => z.GeneralProfile.FreePlay))
                                                   .ForMember(p => p.Availability, d => d.MapFrom(z => z.Availability))
                                                   .ForMember(p => p.Biography, d => d.MapFrom(z => z.GeneralProfile.Biography))
                                                   .ForMember(p => p.Birthday, d => d.MapFrom(z => z.GeneralProfile.Birthday))
                                                   .ForMember(p => p.City, d => d.MapFrom(z => z.GeneralProfile.City))
                                                   .ForMember(p => p.Tags, d => d.MapFrom(z => z.Tags));
}
