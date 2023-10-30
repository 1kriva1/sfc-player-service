using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Settings;

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

    public static readonly FootballPosition[] FOOTBALL_POSITIONS = new FootballPosition[] {
                        new FootballPosition { Id = 0, Title = "Goalkeeper" },
                        new FootballPosition { Id = 1, Title = "Defender" },
                        new FootballPosition { Id = 2, Title = "Midfielder" },
                        new FootballPosition { Id = 3, Title = "Forward" }
    };

    public static readonly WorkingFoot[] WORKING_FOOTS = new WorkingFoot[] {
                        new WorkingFoot { Id = 0, Title = "Right" },
                        new WorkingFoot { Id = 1, Title = "Left" },
                        new WorkingFoot { Id = 2, Title = "Both" }
    };

    public static readonly GameStyle[] GAME_STYLES = new GameStyle[] {
                        new GameStyle { Id = 0, Title = "Defend" },
                        new GameStyle { Id = 1, Title = "Attacking" },
                        new GameStyle { Id = 2, Title = "Aggresive" },
                        new GameStyle { Id = 3, Title = "Control" },
                        new GameStyle { Id = 4, Title = "CounterAttacks" }
    };

    public static readonly StatCategory[] STAT_CATEGORIES = new StatCategory[] {
                        new StatCategory { Id = 0, Title = "Pace" },
                        new StatCategory { Id = 1, Title = "Shooting" },
                        new StatCategory { Id = 2, Title = "Passing" },
                        new StatCategory { Id = 3, Title = "Dribbling" },
                        new StatCategory { Id = 4, Title = "Defending" },
                        new StatCategory { Id = 5, Title = "Physicality" }
    };

    public static readonly StatSkill[] STAT_SKILLS = new StatSkill[] {
                        new StatSkill { Id = 0, Title = "Physical" },
                        new StatSkill { Id = 1, Title = "Mental" },
                        new StatSkill { Id = 2, Title = "Skill" }
    };

    public static readonly StatType[] STAT_TYPES = new StatType[] {
                        new StatType { Id = 0, Title = "Acceleration", CategoryId = 0, SkillId = 0 },
                        new StatType { Id = 1, Title = "SprintSpeed", CategoryId = 0, SkillId = 0 },
                        new StatType { Id = 2, Title = "Positioning", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 3, Title = "Finishing", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 4, Title = "ShotPower", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 5, Title = "LongShots", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 6, Title = "Volleys", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 7, Title = "Penalties", CategoryId = 1, SkillId = 2 },
                        new StatType { Id = 8, Title = "Vision", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 9, Title = "Crossing", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 10, Title = "FkAccuracy", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 11, Title = "ShortPassing", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 12, Title = "LongPassing", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 13, Title = "Curve", CategoryId = 2, SkillId = 2 },
                        new StatType { Id = 14, Title = "Agility", CategoryId = 3, SkillId = 0 },
                        new StatType { Id = 15, Title = "Balance", CategoryId = 3, SkillId = 0},
                        new StatType { Id = 16, Title = "Reactions", CategoryId = 3, SkillId = 0 },
                        new StatType { Id = 17, Title = "BallControl", CategoryId = 3, SkillId = 2 },
                        new StatType { Id = 18, Title = "Dribbling", CategoryId = 3, SkillId = 2 },
                        new StatType { Id = 19, Title = "Composure", CategoryId = 3, SkillId = 1 },
                        new StatType { Id = 20, Title = "Interceptions", CategoryId = 4, SkillId = 2},
                        new StatType { Id = 21, Title = "HeadingAccuracy", CategoryId = 4, SkillId = 2 },
                        new StatType { Id = 22, Title = "DefAwarenence", CategoryId = 4, SkillId = 2 },
                        new StatType { Id = 23, Title = "StandingTackle", CategoryId = 4, SkillId = 2 },
                        new StatType { Id = 24, Title = "SlidingTackle", CategoryId = 4, SkillId = 2 },
                        new StatType { Id = 25, Title = "Jumping", CategoryId = 5, SkillId = 0 },
                        new StatType { Id = 26, Title = "Stamina", CategoryId = 5, SkillId = 0 },
                        new StatType { Id = 27, Title = "Strength", CategoryId = 5, SkillId = 0 },
                        new StatType { Id = 28, Title = "Aggression", CategoryId = 5, SkillId = 1 }
    };
}
