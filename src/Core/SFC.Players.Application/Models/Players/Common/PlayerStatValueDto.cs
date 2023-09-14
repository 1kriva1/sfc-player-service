using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Models.Players.Common;
public record PlayerStatValueDto : IMapFromReverse<PlayerStat>
{
    public StatCategory Category { get; set; }

    public StatType Type { get; set; }

    public byte Value { get; set; }
}