using AutoMapper;

using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;

using SystemConvert = System.Convert;

namespace SFC.Players.Application.Common.Mappings.Converters;
public class Base64StringTypeConverter : ITypeConverter<PlayerPhoto?, string?>
{
    public string? Convert(PlayerPhoto? file, string? destination, ResolutionContext context)
    {
        return file != null
            ? $"data:image/{Enum.GetName(typeof(PhotoExtension), file.Extension)!.ToLower()};base64,{SystemConvert.ToBase64String(file.Source)}"
            : null;
    }
}
