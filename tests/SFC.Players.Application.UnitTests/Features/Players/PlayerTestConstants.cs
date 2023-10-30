using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.UnitTests.Features.Players;
public class PlayerTestConstants
{
    public static int STAT_CATEGORIES_COUNT = 6;

    public static int STAT_TYPES_COUNT = 29;

    public static readonly List<PlayerStatValueDto> VALID_STATS = new()
    {
                        new PlayerStatValueDto{ Category = 0, Type = 0, Value = 50 },
                        new PlayerStatValueDto{ Category = 0, Type = 1, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 2, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 3, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 4, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 5, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 6, Value = 50 },
                        new PlayerStatValueDto{ Category = 1, Type = 7, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 8, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 9, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 10, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 11, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 12, Value = 50 },
                        new PlayerStatValueDto{ Category = 2, Type = 13, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 14, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 15, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 16, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 17, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 18, Value = 50 },
                        new PlayerStatValueDto{ Category = 3, Type = 19, Value = 50 },
                        new PlayerStatValueDto{ Category = 4, Type = 20, Value = 50 },
                        new PlayerStatValueDto{ Category = 4, Type = 21, Value = 50 },
                        new PlayerStatValueDto{ Category = 4, Type = 22, Value = 50 },
                        new PlayerStatValueDto{ Category = 4, Type = 23, Value = 50 },
                        new PlayerStatValueDto{ Category = 4, Type = 24, Value = 50 },
                        new PlayerStatValueDto{ Category = 5, Type = 25, Value = 50 },
                        new PlayerStatValueDto{ Category = 5, Type = 26, Value = 50 },
                        new PlayerStatValueDto{ Category = 5, Type = 27, Value = 50 },
                        new PlayerStatValueDto{ Category = 5, Type = 28, Value = 50 }
    };

    public static readonly IReadOnlyList<StatType> STAT_TYPES = new List<StatType>()
    {
                        new StatType{ CategoryId = 0, Id = 0 },
                        new StatType{ CategoryId = 0, Id = 1 },
                        new StatType{ CategoryId = 1, Id = 2 },
                        new StatType{ CategoryId = 1, Id = 3 },
                        new StatType{ CategoryId = 1, Id = 4 },
                        new StatType{ CategoryId = 1, Id = 5 },
                        new StatType{ CategoryId = 1, Id = 6 },
                        new StatType{ CategoryId = 1, Id = 7 },
                        new StatType{ CategoryId = 2, Id = 8 },
                        new StatType{ CategoryId = 2, Id = 9 },
                        new StatType{ CategoryId = 2, Id = 10 },
                        new StatType{ CategoryId = 2, Id = 11 },
                        new StatType{ CategoryId = 2, Id = 12 },
                        new StatType{ CategoryId = 2, Id = 13 },
                        new StatType{ CategoryId = 3, Id = 14 },
                        new StatType{ CategoryId = 3, Id = 15 },
                        new StatType{ CategoryId = 3, Id = 16 },
                        new StatType{ CategoryId = 3, Id = 17 },
                        new StatType{ CategoryId = 3, Id = 18 },
                        new StatType{ CategoryId = 3, Id = 19 },
                        new StatType{ CategoryId = 4, Id = 20 },
                        new StatType{ CategoryId = 4, Id = 21 },
                        new StatType{ CategoryId = 4, Id = 22 },
                        new StatType{ CategoryId = 4, Id = 23 },
                        new StatType{ CategoryId = 4, Id = 24 },
                        new StatType{ CategoryId = 5, Id = 25 },
                        new StatType{ CategoryId = 5, Id = 26 },
                        new StatType{ CategoryId = 5, Id = 27 },
                        new StatType{ CategoryId = 5, Id = 28 }
    };

    public static BasePlayerDto GetValidPlayer() => new()
    {
        Profile = new PlayerProfileDto
        {
            General = new PlayerGeneralProfileDto
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City Value"
            },
            Football = new PlayerFootballProfileDto()
        },
        Stats = new PlayerStatsDto
        {
            Points = new PlayerStatPointsDto(),
            Values = VALID_STATS
        }
    };
}
