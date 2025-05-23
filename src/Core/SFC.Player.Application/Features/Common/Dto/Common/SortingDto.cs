using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Features.Common.Dto.Common;
public class SortingDto
{
    public string Name { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
