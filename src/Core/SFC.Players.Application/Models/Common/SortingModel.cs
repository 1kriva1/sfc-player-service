using SFC.Players.Application.Common.Enums;

namespace SFC.Players.Application.Models.Common;
public class SortingModel
{
    public string Name { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
