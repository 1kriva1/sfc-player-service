using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;

namespace SFC.Player.Application.Models.Players.Common;

/// <summary>
/// Player's **football** profile model.
/// </summary>
public class PlayerFootballProfileModel : IMapFrom<PlayerFootballProfileDto>
{
    /// <summary>
    /// Height.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Weight.
    /// </summary>
    public int? Weight { get; set; }

    /// <summary>
    /// Position on field.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Second level **position** on field.
    /// </summary>
    public int? AdditionalPosition { get; set; }

    /// <summary>
    /// Describe what **foot** player prefer to use.
    /// </summary>
    public int? WorkingFoot { get; set; }

    /// <summary>
    /// Preferred **number** on T-Shirt.
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// **Style** of play.
    /// </summary>
    public int? GameStyle { get; set; }

    /// <summary>
    /// **Dribbling** skill value.
    /// </summary>
    public int? Skill { get; set; }

    /// <summary>
    /// **Week foot** skill value.
    /// </summary>
    public int? WeakFoot { get; set; }

    /// <summary>
    /// Physical condition value.
    /// </summary>
    public int? PhysicalCondition { get; set; }
}
