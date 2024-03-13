using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Features.Common.Dto;
public class SortingDto : IMapFrom<SortingModel>
{
    public string Name { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
