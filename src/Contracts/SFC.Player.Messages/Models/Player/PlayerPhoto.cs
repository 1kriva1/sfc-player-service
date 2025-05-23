namespace SFC.Player.Messages.Models.Player;
public class PlayerPhoto
{
#pragma warning disable CA1819 // Properties should not return arrays
    public byte[] Source { get; set; } = [];
#pragma warning restore CA1819 // Properties should not return arrays

    public required string Name { get; set; }

    public required string Extension { get; set; }

    public int Size { get; set; }
}
