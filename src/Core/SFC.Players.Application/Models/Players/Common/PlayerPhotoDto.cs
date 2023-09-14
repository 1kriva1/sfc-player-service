using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerPhotoDto
{
    public byte[] Source { get; set; } = Array.Empty<byte>();

    public string Name { get; set; } = string.Empty;

    public PhotoExtension Extension { get; set; }

    public int Size { get; set; }
}
