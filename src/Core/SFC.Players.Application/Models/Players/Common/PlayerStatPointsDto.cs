using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common;

public record PlayerStatPointsDto: IMapFromReverse<PlayerStatPoints>
{
    public int Available { get; set; }

    public int Used { get; set; }
}
