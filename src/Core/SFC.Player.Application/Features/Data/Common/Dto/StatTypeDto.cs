using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Data.Common.Dto;
public class StatTypeDto : BaseDataDto, IMapTo<StatType>
{
    public int CategoryId { get; set; }

    public int SkillId { get; set; }
}
