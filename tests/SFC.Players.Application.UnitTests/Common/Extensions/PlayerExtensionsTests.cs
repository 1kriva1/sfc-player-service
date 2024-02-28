using SFC.Players.Application.Common.Extensions;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.UnitTests.Common.Extensions;
public class PlayerExtensionsTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");

    [Fact]
    [Trait("Extension", "Player")]
    public void Extension_Player_ShouldAttachUser()
    {
        // Arrange
        Player player = new();

        // Act
        Player updatedPlayer = player.SetUser(MOCK_USER_ID);

        // Assert
        Assert.NotNull(updatedPlayer);
        Assert.NotNull(player.User);
        Assert.Equal(MOCK_USER_ID, player.User.UserId);
    }

    [Fact]
    [Trait("Extension", "Player")]
    public void Extension_Player_ShouldAttachTypeToPlayerStats()
    {
        // Arrange
        StatType statType = new() { Id = 10, Title = "Test" };
        List<StatType> statTypes = new() { statType };
        Player player = new();

        // Act
        player.Stats.Add(new PlayerStat
        {
            Id = 0,
            Type = new StatType { Id = 10 }
        });
        Player updatedPlayer = player.SetStatTypes(statTypes);
        PlayerStat? playerStat = player.Stats.FirstOrDefault(st => st.Type.Id == 10);

        // Assert
        Assert.NotNull(updatedPlayer);
        Assert.NotNull(playerStat);
        Assert.Equal(statType, playerStat.Type);
    }
}
