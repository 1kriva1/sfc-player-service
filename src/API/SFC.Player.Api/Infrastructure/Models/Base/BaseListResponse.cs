namespace SFC.Player.Api.Infrastructure.Models.Base;

/// <summary>
/// **Base** response model with **list** of items.
/// </summary>
/// <typeparam name="T">Any type of model.</typeparam>
public class BaseListResponse<T> : BaseErrorResponse
{
    /// <summary>
    /// **List** of items.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = [];
}
