namespace SFC.Player.Api.Infrastructure.Models.Common;

/// <summary>
/// **Generic** range limit model.
/// </summary>
/// <typeparam name="T">Any type that can be **compared**.</typeparam>
public class RangeLimitModel<T>
{
    /// <summary>
    /// Range **from**.
    /// </summary>
    public T? From { get; set; }

    /// <summary>
    /// Range **to**.
    /// </summary>
    public T? To { get; set; }
}
