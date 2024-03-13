namespace SFC.Player.Application.Common.Constants;

public static class CommonConstants
{
    public const string RESOURCE_PATH = "Resources";

    public static readonly string[] SUPPORTED_CULTURES = { "en-GB", "uk-UA" };

    public const string PAGINATION_HEADER_KEY = "X-Pagination";

    public const int PERCENTAGE_MAX_VALUE = 100;

    public static readonly Tuple<int, int> RANGE_LIMIT = new(0, 100);
}
