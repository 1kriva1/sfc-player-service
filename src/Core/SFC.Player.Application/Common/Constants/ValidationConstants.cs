namespace SFC.Player.Application.Common.Constants;
public static class ValidationConstants
{
    public const int NameValueMaxLength = 150;

    public const int DescriptionValueMaxLength = 1050;

    public const int CityValueMaxLength = 100;

    public const int MaxYearsOld = 99;

    public const int PhotoMaxSizeInBytes = 5242880;

    public const int TagValueMaxLength = 20;

    public const int TagsMaxLength = 50;

    public const int ExtensionValueMaxLength = 20;

    public const int PercentageMaxValue = 100;

    public const int TitleValueMaxLength = 150;

    public static readonly Tuple<int, int> PlayerSizeRange = new(1, 300);

    public static readonly Tuple<int, int> PlayerNumberRange = new(0, 99);

    public static readonly Tuple<int, int> RaitingValueRange = new(0, 5);

    public static readonly Tuple<int, int> StatValueRange = new(0, 100);

    public static readonly Tuple<int, int> RangeLimit = new(0, 100);
}
