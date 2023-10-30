namespace SFC.Players.Application.Models.Players.Common;
public record PlayerStatValueDto
{
    public int Category { get; set; } = default!;

    public int Type { get; set; } = default!;

    public byte Value { get; set; }
}