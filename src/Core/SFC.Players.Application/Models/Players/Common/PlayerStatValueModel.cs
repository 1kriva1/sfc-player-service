using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerStatValueModel : IMapFrom<PlayerStatValueDto>
{
    public int Type { get; set; } = default!;

    public byte Value { get; set; }
}
