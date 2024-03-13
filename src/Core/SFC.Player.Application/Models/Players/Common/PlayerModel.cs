using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerModel : BasePlayerModel, IMapFrom<PlayerDto>
{
    public long Id { get; set; }
}
