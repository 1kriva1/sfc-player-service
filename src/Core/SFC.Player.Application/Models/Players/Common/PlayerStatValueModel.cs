using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerStatValueModel : IMapFrom<PlayerStatValueDto>
{
    public int Type { get; set; } = default!;

    public byte Value { get; set; }
}
