namespace SFC.Player.Application.Common.Constants;
public static class ValidationConstants
{
    public const int NAME_VALUE_MAX_LENGTH = 150;

    public const int DESCRIPTION_VALUE_MAX_LENGTH = 1050;

    public const int CITY_VALUE_MAX_LENGTH = 100;

    public const int MAX_YEARS_OLD = 99;

    public const int PHOTO_MAX_SIZE_IN_BYTES = 5242880;

    public const int TAG_VALUE_MAX_LENGTH = 20;

    public const int TAGS_MAX_LENGTH = 50;

    public static readonly Tuple<int, int> PLAYER_SIZE_RANGE = new(1, 300);

    public static readonly Tuple<int, int> PLAYER_NUMBER_RANGE = new(0, 99);

    public static readonly Tuple<int, int> RAITING_VALUE_RANGE = new(0, 5);

    public static readonly Tuple<int, int> STAT_VALUE_RANGE = new(0, 100);
}
