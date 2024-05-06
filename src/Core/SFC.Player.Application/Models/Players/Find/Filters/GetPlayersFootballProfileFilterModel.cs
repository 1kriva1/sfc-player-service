using SFC.Player.Application.Models.Common;

namespace SFC.Player.Application.Models.Players.Find.Filters;

/// <summary>
/// Get players **football profile filter** model.
/// </summary>
public class GetPlayersFootballProfileFilterModel
{
    /// <summary>
    /// Height.
    /// </summary>
    public RangeLimitModel<short?> Height { get; set; } = default!;

    /// <summary>
    /// Weight.
    /// </summary>
    public RangeLimitModel<short?> Weight { get; set; } = default!;

    /// <summary>
    /// List of **positions** on field.
    /// </summary>
    public IEnumerable<int> Positions { get; set; } = default!;

    /// <summary>
    /// What **foot** player prefer to use.
    /// </summary>
    public int? WorkingFoot { get; set; }

    /// <summary>
    /// **Style** of play.
    /// </summary>
    public IEnumerable<int> GameStyles { get; set; } = default!;

    /// <summary>
    /// **Dribbling** skill value.
    /// </summary>
    public int? Skill { get; set; }

    /// <summary>
    /// Physical condition value.
    /// </summary>
    public int? PhysicalCondition { get; set; }
}
