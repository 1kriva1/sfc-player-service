using System.ComponentModel;

namespace SFC.Player.Domain.Enums.Data;
public enum StatType
{
    Acceleration = 0,
    [Description("Sprint Speed")]
    SprintSpeed = 1,
    Positioning = 2,
    Finishing = 3,
    [Description("Shot Power")]
    ShotPower = 4,
    [Description("Long Shots")]
    LongShots = 5,
    Volleys = 6,
    Penalties = 7,
    Vision = 8,
    Crossing = 9,
    [Description("Fk Accuracy")]
    FkAccuracy = 10,
    [Description("Short Passing")]
    ShortPassing = 11,
    [Description("Long Passing")]
    LongPassing = 12,
    Curve = 13,
    Agility = 14,
    Balance = 15,
    Reactions = 16,
    [Description("Ball Control")]
    BallControl = 17,
    Dribbling = 18,
    Composure = 19,
    Interceptions = 20,
    [Description("Heading Accuracy")]
    HeadingAccuracy = 21,
    [Description("Def Awarenence")]
    DefAwarenence = 22,
    [Description("Standing Tackle")]
    StandingTackle = 23,
    [Description("Sliding Tackle")]
    SlidingTackle = 24,
    Jumping = 25,
    Stamina = 26,
    Strength = 27,
    Aggression = 28
}
