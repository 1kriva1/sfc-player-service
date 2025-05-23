using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerGeneralProfileDto : IMapFrom<PlayerEntity>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public PlayerPhotoDto? Photo { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerGeneralProfileDto>()
                                                   .ForMember(p => p.Photo, d => d.MapFrom(z => z.Photo))
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName));
}
