using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Data.Common.Dto;

namespace SFC.Player.Application.Features.Data.Commands.Reset;
public class ResetDataCommand : Request
{
    public override RequestId RequestId { get => RequestId.ResetData; }

    public IEnumerable<FootballPositionDto> FootballPositions { get; init; } = [];

    public IEnumerable<GameStyleDto> GameStyles { get; init; } = [];

    public IEnumerable<StatCategoryDto> StatCategories { get; init; } = [];

    public IEnumerable<StatSkillDto> StatSkills { get; init; } = [];

    public IEnumerable<StatTypeDto> StatTypes { get; init; } = [];

    public IEnumerable<WorkingFootDto> WorkingFoots { get; init; } = [];
}
