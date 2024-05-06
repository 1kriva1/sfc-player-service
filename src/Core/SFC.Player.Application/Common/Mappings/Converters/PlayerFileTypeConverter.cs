using AutoMapper;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Domain.Enums;

using SystemConvert = System.Convert;

namespace SFC.Player.Application.Common.Mappings.Converters;
public class PlayerFileTypeConverter : ITypeConverter<string?, PlayerPhotoDto?>
{
    private const string FILE_NAME = "Photo";

    public PlayerPhotoDto? Convert(string? base64, PlayerPhotoDto? destination, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(base64))
            return null;

        string base64String = base64[(base64.IndexOf(",") + 1)..];

        PhotoExtension extension = GetBase64FileExtension(base64String);

        byte[] source = SystemConvert.FromBase64String(base64String);

        return new PlayerPhotoDto
        {
            Source = source,
            Size = source.Length,
            Name = FILE_NAME,
            Extension = extension
        };
    }

    public static PhotoExtension GetBase64FileExtension(string base64String)
    {
        string data = base64String[..5];

        return data.ToUpper() switch
        {
            "IVBOR" => PhotoExtension.Png,
            "/9J/4" => PhotoExtension.Jpg,
            "R0lGO" => PhotoExtension.Gif,
            "UKLGR" => PhotoExtension.Webp,
            _ => throw new BadRequestException(Messages.ValidationError, (nameof(PlayerGeneralProfileDto.Photo), Messages.InvalidPhotoExtension))
        };
    }
}
