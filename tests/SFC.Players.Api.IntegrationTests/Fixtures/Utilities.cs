using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;
using SFC.Players.Infrastructure.Persistence;

namespace SFC.Players.Api.IntegrationTests.Fixtures;
public class Utilities
{
    public static void InitializeDbForTests(PlayersDbContext context)
    {
        Player player = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                City = "TestCity"
            },
            FootballProfile = new PlayerFootballProfile
            {
                Position = FootballPosition.Goalkeeper
            }
        };
        Constants.VALID_STATS.ForEach(stat => 
            player.Stats.Add(new PlayerStat { Category = stat.Category, Type = stat.Type, Value = stat.Value }));

        context.Players.Add(player);

        context.SaveChanges();
    }
}
