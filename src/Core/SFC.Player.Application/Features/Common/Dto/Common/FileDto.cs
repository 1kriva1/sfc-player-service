namespace SFC.Player.Application.Features.Common.Dto.Common;

/// <summary>
/// Parent class for all file related DTO.
/// </summary>
public class FileDto
{
    public IEnumerable<byte> Source { get; set; } = [];

    public string Name { get; set; } = string.Empty;

    public PhotoExtensionEnum Extension { get; set; }

    public int Size { get; set; }
}
