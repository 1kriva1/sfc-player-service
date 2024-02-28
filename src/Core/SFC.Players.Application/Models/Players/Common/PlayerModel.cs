using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerModel : BasePlayerModel, IMapFrom<PlayerDto>
{
    public long Id { get; set; }
}
