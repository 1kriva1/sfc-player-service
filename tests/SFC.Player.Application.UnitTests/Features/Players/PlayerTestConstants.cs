using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.UnitTests.Features.Player;
public class PlayerTestConstants
{
    public static int STAT_CATEGORIES_COUNT = 6;

    public static int STAT_TYPES_COUNT = 29;

    public static readonly List<PlayerStatValueDto> VALID_STATS = new()
    {
                        new PlayerStatValueDto{ Type = 0, Value = 50 },
                        new PlayerStatValueDto{ Type = 1, Value = 50 },
                        new PlayerStatValueDto{ Type = 2, Value = 50 },
                        new PlayerStatValueDto{ Type = 3, Value = 50 },
                        new PlayerStatValueDto{ Type = 4, Value = 50 },
                        new PlayerStatValueDto{ Type = 5, Value = 50 },
                        new PlayerStatValueDto{ Type = 6, Value = 50 },
                        new PlayerStatValueDto{ Type = 7, Value = 50 },
                        new PlayerStatValueDto{ Type = 8, Value = 50 },
                        new PlayerStatValueDto{ Type = 9, Value = 50 },
                        new PlayerStatValueDto{ Type = 10, Value = 50 },
                        new PlayerStatValueDto{ Type = 11, Value = 50 },
                        new PlayerStatValueDto{ Type = 12, Value = 50 },
                        new PlayerStatValueDto{ Type = 13, Value = 50 },
                        new PlayerStatValueDto{ Type = 14, Value = 50 },
                        new PlayerStatValueDto{ Type = 15, Value = 50 },
                        new PlayerStatValueDto{ Type = 16, Value = 50 },
                        new PlayerStatValueDto{ Type = 17, Value = 50 },
                        new PlayerStatValueDto{ Type = 18, Value = 50 },
                        new PlayerStatValueDto{ Type = 19, Value = 50 },
                        new PlayerStatValueDto{ Type = 20, Value = 50 },
                        new PlayerStatValueDto{ Type = 21, Value = 50 },
                        new PlayerStatValueDto{ Type = 22, Value = 50 },
                        new PlayerStatValueDto{ Type = 23, Value = 50 },
                        new PlayerStatValueDto{ Type = 24, Value = 50 },
                        new PlayerStatValueDto{ Type = 25, Value = 50 },
                        new PlayerStatValueDto{ Type = 26, Value = 50 },
                        new PlayerStatValueDto{ Type = 27, Value = 50 },
                        new PlayerStatValueDto{ Type = 28, Value = 50 }
    };

    public static readonly IReadOnlyList<StatType> STAT_TYPES = new List<StatType>()
    {
                        new StatType{ Id = 0 },
                        new StatType{ Id = 1 },
                        new StatType{  Id = 2 },
                        new StatType{ Id = 3 },
                        new StatType{ Id = 4 },
                        new StatType{ Id = 5 },
                        new StatType{ Id = 6 },
                        new StatType{ Id = 7 },
                        new StatType{ Id = 8 },
                        new StatType{ Id = 9 },
                        new StatType{ Id = 10 },
                        new StatType{ Id = 11 },
                        new StatType{ Id = 12 },
                        new StatType{ Id = 13 },
                        new StatType{ Id = 14 },
                        new StatType{ Id = 15 },
                        new StatType{ Id = 16 },
                        new StatType{ Id = 17 },
                        new StatType{ Id = 18 },
                        new StatType{ Id = 19 },
                        new StatType{ Id = 20 },
                        new StatType{ Id = 21 },
                        new StatType{ Id = 22 },
                        new StatType{ Id = 23 },
                        new StatType{ Id = 24 },
                        new StatType{ Id = 25 },
                        new StatType{ Id = 26 },
                        new StatType{ Id = 27 },
                        new StatType{ Id = 28 }
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
