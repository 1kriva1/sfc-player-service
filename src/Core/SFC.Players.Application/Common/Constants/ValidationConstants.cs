using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Common.Constants;
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

    public const int STATS_COUNT = 29;

    public static readonly Tuple<int, int> STAT_VALUE_RANGE = new(0, 100);

    public static Dictionary<StatCategory, IEnumerable<StatType>> CATEGORY_TYPE_STAT_RELATIONS = new()
    {
        {
            StatCategory.Pace,
            new List<StatType>{
                StatType.Acceleration, StatType.SprintSpeed
            }
        },
        {
            StatCategory.Shooting,
            new List<StatType>{
                StatType.Positioning, StatType.Finishing, StatType.ShotPower, StatType.LongShots, StatType.Volleys, StatType.Penalties
            }
        },
        {
            StatCategory.Passing,
            new List<StatType>{
                StatType.Vision, StatType.Crossing, StatType.FkAccuracy, StatType.ShortPassing, StatType.LongPassing, StatType.Curve
            }
        },
        {
            StatCategory.Dribbling,
            new List<StatType>{
                StatType.Agility, StatType.Balance, StatType.Reactions, StatType.BallControl, StatType.Dribbling, StatType.Composure
            }
        },
        {
            StatCategory.Defending,
            new List<StatType>{
                StatType.Interceptions, StatType.HeadingAccuracy, StatType.DefAwarenence, StatType.StandingTackle, StatType.SlidingTackle
            }
        },
        {
            StatCategory.Physicality,
            new List<StatType>{
                StatType.Jumping, StatType.Stamina, StatType.Strength, StatType.Aggresion
            }
        }
    };
}
