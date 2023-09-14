using SFC.Players.Application.Models.Configurations;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Api.IntegrationTests.Fixtures;
public static class Constants
{
    public static Guid USER_ID = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
    public static JwtSettings JWT_SETTINGS = new()
    {
        Issuer = "GloboTicketIdentity",
        Audience = "GloboTicketIdentityUser",
        Key = "73AE92E6113F4369A713A94C5A9C6B15"
    };
    public static readonly List<PlayerStatValueDto> VALID_STATS = new()
    {
                        new PlayerStatValueDto{ Category = StatCategory.Pace, Type = StatType.Acceleration, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Pace, Type = StatType.SprintSpeed, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.Positioning, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.Finishing, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.ShotPower, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.LongShots, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.Volleys, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Shooting, Type = StatType.Penalties, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.Vision, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.Crossing, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.FkAccuracy, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.ShortPassing, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.LongPassing, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Passing, Type = StatType.Curve, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.Agility, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.Balance, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.Reactions, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.BallControl, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.Dribbling, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Dribbling, Type = StatType.Composure, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Defending, Type = StatType.Interceptions, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Defending, Type = StatType.HeadingAccuracy, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Defending, Type = StatType.DefAwarenence, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Defending, Type = StatType.StandingTackle, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Defending, Type = StatType.SlidingTackle, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Physicality, Type = StatType.Jumping, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Physicality, Type = StatType.Stamina, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Physicality, Type = StatType.Strength, Value = 50 },
                        new PlayerStatValueDto{ Category = StatCategory.Physicality, Type = StatType.Aggresion, Value = 50 }
    };
}
