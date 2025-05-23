using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Converters.File;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;
public class PlayerPhotoDto : FileDto, IMapFromReverse<PlayerPhoto>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerPhoto, PlayerPhotoDto>();

        profile.CreateMap<string?, PlayerPhotoDto?>()
               .ConvertUsing<Base64StringToFileTypeConverter<PlayerPhotoDto>>();

        profile.CreateMap<PlayerPhotoDto?, string?>()
               .ConvertUsing<FileToBase64StringTypeConverter<PlayerPhotoDto>>();

        profile.CreateMap<PlayerPhotoDto, PlayerPhoto>()
               .IgnoreAllNonExisting();
    }
}
