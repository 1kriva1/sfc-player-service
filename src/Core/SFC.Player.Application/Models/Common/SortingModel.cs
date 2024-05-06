using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Models.Common;

/// <summary>
/// **Sorting** model.
/// </summary>
public class SortingModel
{
    /// <summary>
    /// **Name of property** by which sorting will be performed.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Sorting **direction** (ascending or descending).
    /// </summary>
    public SortingDirection Direction { get; set; }
}
