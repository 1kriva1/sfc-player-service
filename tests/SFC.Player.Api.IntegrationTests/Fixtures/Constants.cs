using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Api.IntegrationTests.Fixtures;
public static class Constants
{
    public static readonly JwtSettings JwtSettings = new()
    {
        Issuer = "GloboTicketIdentity",
        Audience = "GloboTicketIdentityUser",
        Key = "73AE92E6113F4369A713A94C5A9C6B15"
    };

    public static readonly List<PlayerStatValueModel> VALID_STATS = new()
    {
                        new PlayerStatValueModel{ Type = 0, Value = 50 },
                        new PlayerStatValueModel{ Type = 1, Value = 50 },
                        new PlayerStatValueModel{ Type = 2, Value = 50 },
                        new PlayerStatValueModel{ Type = 3, Value = 50 },
                        new PlayerStatValueModel{ Type = 4, Value = 50 },
                        new PlayerStatValueModel{ Type = 5, Value = 50 },
                        new PlayerStatValueModel{ Type = 6, Value = 50 },
                        new PlayerStatValueModel{ Type = 7, Value = 50 },
                        new PlayerStatValueModel{ Type = 8, Value = 50 },
                        new PlayerStatValueModel{ Type = 9, Value = 50 },
                        new PlayerStatValueModel{ Type = 10, Value = 50 },
                        new PlayerStatValueModel{ Type = 11, Value = 50 },
                        new PlayerStatValueModel{ Type = 12, Value = 50 },
                        new PlayerStatValueModel{ Type = 13, Value = 50 },
                        new PlayerStatValueModel{ Type = 14, Value = 50 },
                        new PlayerStatValueModel{ Type = 15, Value = 50 },
                        new PlayerStatValueModel{ Type = 16, Value = 50 },
                        new PlayerStatValueModel{ Type = 17, Value = 50 },
                        new PlayerStatValueModel{ Type = 18, Value = 50 },
                        new PlayerStatValueModel{ Type = 19, Value = 50 },
                        new PlayerStatValueModel{ Type = 20, Value = 50 },
                        new PlayerStatValueModel{ Type = 21, Value = 50 },
                        new PlayerStatValueModel{ Type = 22, Value = 50 },
                        new PlayerStatValueModel{ Type = 23, Value = 50 },
                        new PlayerStatValueModel{ Type = 24, Value = 50 },
                        new PlayerStatValueModel{ Type = 25, Value = 50 },
                        new PlayerStatValueModel{ Type = 26, Value = 50 },
                        new PlayerStatValueModel{ Type = 27, Value = 50 },
                        new PlayerStatValueModel{ Type = 28, Value = 50 }
    };

    public static readonly FootballPosition[] FOOTBALL_POSITIONS = new FootballPosition[] {
                        new() { Id = 0, Title = "Goalkeeper" },
                        new() { Id = 1, Title = "Defender" },
                        new() { Id = 2, Title = "Midfielder" },
                        new() { Id = 3, Title = "Forward" }
    };

    public static readonly WorkingFoot[] WORKING_FOOTS = new WorkingFoot[] {
                        new() { Id = 0, Title = "Right" },
                        new() { Id = 1, Title = "Left" },
                        new() { Id = 2, Title = "Both" }
    };

    public static readonly GameStyle[] GAME_STYLES = new GameStyle[] {
                        new() { Id = 0, Title = "Defend" },
                        new() { Id = 1, Title = "Attacking" },
                        new() { Id = 2, Title = "Aggresive" },
                        new() { Id = 3, Title = "Control" },
                        new() { Id = 4, Title = "CounterAttacks" }
    };

    public static readonly StatCategory[] STAT_CATEGORIES = new StatCategory[] {
                        new() { Id = 0, Title = "Pace" },
                        new() { Id = 1, Title = "Shooting" },
                        new() { Id = 2, Title = "Passing" },
                        new() { Id = 3, Title = "Dribbling" },
                        new() { Id = 4, Title = "Defending" },
                        new() { Id = 5, Title = "Physicality" }
    };

    public static readonly StatSkill[] STAT_SKILLS = new StatSkill[] {
                        new() { Id = 0, Title = "Physical" },
                        new() { Id = 1, Title = "Mental" },
                        new() { Id = 2, Title = "Skill" }
    };

    public static readonly StatType[] STAT_TYPES = new StatType[] {
                        new() { Id = 0, Title = "Acceleration", Category = STAT_CATEGORIES[0], Skill = STAT_SKILLS[0] },
                        new() { Id = 1, Title = "SprintSpeed", Category = STAT_CATEGORIES[0], Skill = STAT_SKILLS[0]},
                        new() { Id = 2, Title = "Positioning", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 3, Title = "Finishing", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 4, Title = "ShotPower", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 5, Title = "LongShots", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 6, Title = "Volleys", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 7, Title = "Penalties", Category = STAT_CATEGORIES[1], Skill = STAT_SKILLS[2]},
                        new() { Id = 8, Title = "Vision", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 9, Title = "Crossing", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 10, Title = "FkAccuracy", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 11, Title = "ShortPassing", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 12, Title = "LongPassing", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 13, Title = "Curve", Category = STAT_CATEGORIES[2], Skill = STAT_SKILLS[2]},
                        new() { Id = 14, Title = "Agility", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[0]},
                        new() { Id = 15, Title = "Balance", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[0]},
                        new() { Id = 16, Title = "Reactions", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[0]},
                        new() { Id = 17, Title = "BallControl", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[2]},
                        new() { Id = 18, Title = "Dribbling", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[2]},
                        new() { Id = 19, Title = "Composure", Category = STAT_CATEGORIES[3], Skill = STAT_SKILLS[1]},
                        new() { Id = 20, Title = "Interceptions", Category = STAT_CATEGORIES[4], Skill = STAT_SKILLS[2]},
                        new() { Id = 21, Title = "HeadingAccuracy", Category = STAT_CATEGORIES[4], Skill = STAT_SKILLS[2]},
                        new() { Id = 22, Title = "DefAwarenence", Category = STAT_CATEGORIES[4], Skill = STAT_SKILLS[2]},
                        new() { Id = 23, Title = "StandingTackle", Category = STAT_CATEGORIES[4], Skill = STAT_SKILLS[2]},
                        new() { Id = 24, Title = "SlidingTackle", Category = STAT_CATEGORIES[4], Skill = STAT_SKILLS[2]},
                        new() { Id = 25, Title = "Jumping", Category = STAT_CATEGORIES[5], Skill = STAT_SKILLS[0]},
                        new() { Id = 26, Title = "Stamina", Category = STAT_CATEGORIES[5], Skill = STAT_SKILLS[0]},
                        new() { Id = 27, Title = "Strength", Category = STAT_CATEGORIES[5], Skill = STAT_SKILLS[0]},
                        new() { Id = 28, Title = "Aggression", Category = STAT_CATEGORIES[5], Skill = STAT_SKILLS[1]}
    };
}
