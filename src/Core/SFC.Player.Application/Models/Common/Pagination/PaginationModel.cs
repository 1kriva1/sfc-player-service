namespace SFC.Player.Application.Models.Common.Pagination;

/// <summary>
/// **Pagination** model.
/// </summary>
public class PaginationModel
{
    /// <summary>
    /// Requested **page**.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Requested page **size**.
    /// </summary>
    public int Size { get; set; }
}
