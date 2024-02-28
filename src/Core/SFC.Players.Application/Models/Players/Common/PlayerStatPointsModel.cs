using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerStatPointsModel : IMapFrom<PlayerStatPointsDto>
{
    public int Available { get; set; }

    public int Used { get; set; }
}
