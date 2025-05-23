using AutoMapper;

using SFC.Player.Application.Features.Common.Dto.Common;

using SystemConvert = System.Convert;

namespace SFC.Player.Application.Common.Mappings.Converters.File;
public class FileToBase64StringTypeConverter<T> : ITypeConverter<T?, string?> where T : FileDto
{
    public string? Convert(T? source, string? destination, ResolutionContext context)
    {
        return source != null
            ? $"data:image/{Enum.GetName(typeof(PhotoExtensionEnum), source.Extension)!.ToUpperInvariant()};base64,{SystemConvert.ToBase64String(source.Source.ToArray())}"
            : null;
    }
}
