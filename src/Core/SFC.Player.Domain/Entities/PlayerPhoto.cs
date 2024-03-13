namespace SFC.Player.Domain.Entities;
public class PlayerPhoto : BasePlayerEntity
{
    public byte[] Source { get; set; } = Array.Empty<byte>();

    public string Name { get; set; } = string.Empty;

    public PhotoExtension Extension { get; set; }

    public int Size { get; set; }
}
