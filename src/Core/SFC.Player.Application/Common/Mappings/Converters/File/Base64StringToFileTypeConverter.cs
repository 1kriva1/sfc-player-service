using System.Globalization;

using AutoMapper;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Features.Common.Dto.Common;

using SystemConvert = System.Convert;

namespace SFC.Player.Application.Common.Mappings.Converters.File;
public class Base64StringToFileTypeConverter<T> : ITypeConverter<string?, T?> where T : FileDto, new()
{
    private const string FILE_NAME = "File";

    public T? Convert(string? source, T? destination, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        string base64String = source[(source.IndexOf(',', StringComparison.InvariantCultureIgnoreCase) + 1)..];

        PhotoExtensionEnum extension = GetBase64FileExtension(base64String);

        byte[] result = SystemConvert.FromBase64String(base64String);

        return new T
        {
            Source = result,
            Size = source.Length,
            Name = FILE_NAME,
            Extension = extension
        };
    }

    public PhotoExtensionEnum GetBase64FileExtension(string base64String)
    {
        string data = base64String[..5];

        return data.ToUpper(CultureInfo.InvariantCulture) switch
        {
            "IVBOR" => PhotoExtensionEnum.Png,
            "/9J/4" => PhotoExtensionEnum.Jpg,
            "R0lGO" => PhotoExtensionEnum.Gif,
            "UKLGR" => PhotoExtensionEnum.Webp,
            _ => throw new BadRequestException(Localization.ValidationError, (FILE_NAME, Localization.FileExtensionInvalid))
        };
    }
}
