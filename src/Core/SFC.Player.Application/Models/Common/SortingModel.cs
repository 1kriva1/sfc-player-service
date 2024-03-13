using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Models.Common;
public class SortingModel
{
    public string Name { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
