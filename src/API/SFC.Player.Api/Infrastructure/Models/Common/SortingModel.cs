using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Api.Infrastructure.Models.Common;

/// <summary>
/// **Sorting** model.
/// </summary>
public class SortingModel : IMapTo<SortingDto>
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
