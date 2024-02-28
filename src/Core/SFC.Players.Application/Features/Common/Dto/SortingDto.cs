using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Common;

namespace SFC.Players.Application.Features.Common.Dto;
public class SortingDto : IMapFrom<SortingModel>
{
    public string Name { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
