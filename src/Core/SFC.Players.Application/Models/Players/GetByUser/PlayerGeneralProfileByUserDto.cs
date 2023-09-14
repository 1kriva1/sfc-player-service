using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.GetByUser;
public class PlayerGeneralProfileByUserDto : IMapFrom<Player>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Player, PlayerGeneralProfileByUserDto>()
                                                   .ForMember(p => p.Photo, d => d.MapFrom(z => z.Photo))
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName));
}
