using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Common.Mappings.Converters;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Enums;

namespace SFC.Player.Application.Features.Players.Common.Dto;
public class PlayerPhotoDto : IMapFrom<PlayerPhoto>
{
    public byte[] Source { get; set; } = Array.Empty<byte>();

    public string Name { get; set; } = string.Empty;

    public PhotoExtension Extension { get; set; }

    public int Size { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerPhoto, PlayerPhotoDto>();

        profile.CreateMap<string?, PlayerPhotoDto?>()
            .ConvertUsing<PlayerFileTypeConverter>();

        profile.CreateMap<PlayerPhotoDto?, string?>()
            .ConvertUsing<Base64StringTypeConverter>();
    }
}
