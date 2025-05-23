//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Settings;
//using SFC.Player.Api.Infrastructure.Models.Players.Common;

//namespace SFC.Player.Api.IntegrationTests.Fixtures;
//public static class Constants
//{
//    public const string API_PLAYERS = "/api/players";

//    public const string PLAYER_ACCESS_TOKEN_VALID_0 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImIwNzg4ZDJmLTgwMDMtNDNjMS05MmE0LWVkYzc2YTdjNWRkZSIsInN1YiI6ImIwNzg4ZDJmLTgwMDMtNDNjMS05MmE0LWVkYzc2YTdjNWRkZSIsImp0aSI6ImZhMTdlNjNiIiwic2NvcGUiOlsic2ZjLnBsYXllci5mdWxsIiwicHJvZmlsZSIsIm9wZW5pZCIsIm9mZmxpbmVfYWNjZXNzIl0sImF1ZCI6InNmYy5wbGF5ZXIiLCJuYmYiOjE3MjAwMDM5MTYsImV4cCI6MTc1MTUzOTkxNiwiaWF0IjoxNzIwMDAzOTE4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjY2In0.KnUXpI4TD4wfKUSJTGd8Ez93-GDz_LAV4DpfZv0Am8o";

//    // for own player policy testing
//    public const string PLAYER_ACCESS_TOKEN_VALID_1 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjgzNkE3RkI5LTczRkUtNDEwNy05MUYyLTREOTE2MTU4RTI0MiIsInN1YiI6IjgzNkE3RkI5LTczRkUtNDEwNy05MUYyLTREOTE2MTU4RTI0MiIsImp0aSI6Ijg4MWI5NGRlIiwic2NvcGUiOlsic2ZjLnBsYXllci5mdWxsIiwicHJvZmlsZSIsIm9wZW5pZCIsIm9mZmxpbmVfYWNjZXNzIl0sImF1ZCI6InNmYy5wbGF5ZXIiLCJuYmYiOjE3MjAwMTM2MzUsImV4cCI6MTc1MTU0OTYzNSwiaWF0IjoxNzIwMDEzNjM2LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjY2In0.3So61-18gR_T13Caj5tlJiz3QHAr6-LY9WRixLVBMa4";

//    //without scope
//    public const string PLAYER_ACCESS_TOKEN_FORBIDDEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImIwNzg4ZDJmLTgwMDMtNDNjMS05MmE0LWVkYzc2YTdjNWRkZSIsInN1YiI6ImIwNzg4ZDJmLTgwMDMtNDNjMS05MmE0LWVkYzc2YTdjNWRkZSIsImp0aSI6IjNjYTk5OTE0Iiwic2NvcGUiOlsicHJvZmlsZSIsIm9wZW5pZCIsIm9mZmxpbmVfYWNjZXNzIl0sImF1ZCI6InNmYy5wbGF5ZXIiLCJuYmYiOjE3MjAwMDM5MzcsImV4cCI6MTc1MTUzOTkzNywiaWF0IjoxNzIwMDAzOTM4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjY2In0.jn3j3PrS0VL6LXD-VgdKuGf0_70IcGxQ6kULk9ueKjw";

//    public static readonly List<PlayerStatValueModel> VALID_STATS =
//    [
//                        new PlayerStatValueModel{ Type = 0, Value = 50 },
//                        new PlayerStatValueModel{ Type = 1, Value = 50 },
//                        new PlayerStatValueModel{ Type = 2, Value = 50 },
//                        new PlayerStatValueModel{ Type = 3, Value = 50 },
//                        new PlayerStatValueModel{ Type = 4, Value = 50 },
//                        new PlayerStatValueModel{ Type = 5, Value = 50 },
//                        new PlayerStatValueModel{ Type = 6, Value = 50 },
//                        new PlayerStatValueModel{ Type = 7, Value = 50 },
//                        new PlayerStatValueModel{ Type = 8, Value = 50 },
//                        new PlayerStatValueModel{ Type = 9, Value = 50 },
//                        new PlayerStatValueModel{ Type = 10, Value = 50 },
//                        new PlayerStatValueModel{ Type = 11, Value = 50 },
//                        new PlayerStatValueModel{ Type = 12, Value = 50 },
//                        new PlayerStatValueModel{ Type = 13, Value = 50 },
//                        new PlayerStatValueModel{ Type = 14, Value = 50 },
//                        new PlayerStatValueModel{ Type = 15, Value = 50 },
//                        new PlayerStatValueModel{ Type = 16, Value = 50 },
//                        new PlayerStatValueModel{ Type = 17, Value = 50 },
//                        new PlayerStatValueModel{ Type = 18, Value = 50 },
//                        new PlayerStatValueModel{ Type = 19, Value = 50 },
//                        new PlayerStatValueModel{ Type = 20, Value = 50 },
//                        new PlayerStatValueModel{ Type = 21, Value = 50 },
//                        new PlayerStatValueModel{ Type = 22, Value = 50 },
//                        new PlayerStatValueModel{ Type = 23, Value = 50 },
//                        new PlayerStatValueModel{ Type = 24, Value = 50 },
//                        new PlayerStatValueModel{ Type = 25, Value = 50 },
//                        new PlayerStatValueModel{ Type = 26, Value = 50 },
//                        new PlayerStatValueModel{ Type = 27, Value = 50 },
//                        new PlayerStatValueModel{ Type = 28, Value = 50 }
//    ];

//    public static readonly FootballPosition[] FOOTBALL_POSITIONS = [
//                        new() { Id = 0, Title = "Goalkeeper" },
//                        new() { Id = 1, Title = "Defender" },
//                        new() { Id = 2, Title = "Midfielder" },
//                        new() { Id = 3, Title = "Forward" }
//    ];

//    public static readonly WorkingFoot[] WORKING_FOOTS = [
//                        new() { Id = 0, Title = "Right" },
//                        new() { Id = 1, Title = "Left" },
//                        new() { Id = 2, Title = "Both" }
//    ];

//    public static readonly GameStyle[] GAME_STYLES = [
//                        new() { Id = 0, Title = "Defend" },
//                        new() { Id = 1, Title = "Attacking" },
//                        new() { Id = 2, Title = "Aggresive" },
//                        new() { Id = 3, Title = "Control" },
//                        new() { Id = 4, Title = "CounterAttacks" }
//    ];

//    public static readonly StatCategory[] STAT_CATEGORIES = [
//                        new() { Id = 0, Title = "Pace" },
//                        new() { Id = 1, Title = "Shooting" },
//                        new() { Id = 2, Title = "Passing" },
//                        new() { Id = 3, Title = "Dribbling" },
//                        new() { Id = 4, Title = "Defending" },
//                        new() { Id = 5, Title = "Physicality" }
//    ];

//    public static readonly StatSkill[] STAT_SKILLS = [
//                        new() { Id = 0, Title = "Physical" },
//                        new() { Id = 1, Title = "Mental" },
//                        new() { Id = 2, Title = "SkillId" }
//    ];

//    public static readonly StatType[] STAT_TYPES = [
//                        new() { Id = 0, Title = "Acceleration", CategoryId = STAT_CATEGORIES[0].Id, SkillId = STAT_SKILLS[0].Id },
//                        new() { Id = 1, Title = "SprintSpeed", CategoryId = STAT_CATEGORIES[0].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 2, Title = "Positioning", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 3, Title = "Finishing", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 4, Title = "ShotPower", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 5, Title = "LongShots", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 6, Title = "Volleys", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 7, Title = "Penalties", CategoryId = STAT_CATEGORIES[1].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 8, Title = "Vision", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 9, Title = "Crossing", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 10, Title = "FkAccuracy", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 11, Title = "ShortPassing", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 12, Title = "LongPassing", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 13, Title = "Curve", CategoryId = STAT_CATEGORIES[2].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 14, Title = "Agility", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 15, Title = "Balance", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 16, Title = "Reactions", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 17, Title = "BallControl", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 18, Title = "Dribbling", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 19, Title = "Composure", CategoryId = STAT_CATEGORIES[3].Id, SkillId = STAT_SKILLS[1].Id},
//                        new() { Id = 20, Title = "Interceptions", CategoryId = STAT_CATEGORIES[4].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 21, Title = "HeadingAccuracy", CategoryId = STAT_CATEGORIES[4].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 22, Title = "DefAwarenence", CategoryId = STAT_CATEGORIES[4].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 23, Title = "StandingTackle", CategoryId = STAT_CATEGORIES[4].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 24, Title = "SlidingTackle", CategoryId = STAT_CATEGORIES[4].Id, SkillId = STAT_SKILLS[2].Id},
//                        new() { Id = 25, Title = "Jumping", CategoryId = STAT_CATEGORIES[5].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 26, Title = "Stamina", CategoryId = STAT_CATEGORIES[5].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 27, Title = "Strength", CategoryId = STAT_CATEGORIES[5].Id, SkillId = STAT_SKILLS[0].Id},
//                        new() { Id = 28, Title = "Aggression", CategoryId = STAT_CATEGORIES[5].Id, SkillId = STAT_SKILLS[1].Id}
//    ];
//}
