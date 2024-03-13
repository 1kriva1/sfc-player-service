using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
public class PlayerGeneralProfileByUserDto : IMapFrom<PlayerEntity>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public PlayerPhotoDto? Photo { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<PlayerEntity, PlayerGeneralProfileByUserDto>()
                                                   .ForMember(p => p.Photo, d => d.MapFrom(z => z.Photo))
                                                   .ForMember(p => p.FirstName, d => d.MapFrom(z => z.GeneralProfile.FirstName))
                                                   .ForMember(p => p.LastName, d => d.MapFrom(z => z.GeneralProfile.LastName));
}
