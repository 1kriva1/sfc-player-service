//using SFC.Player.Application.Common.Extensions;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Domain.Entities.Identity;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;

//namespace SFC.Player.Application.UnitTests.Common.Extensions;
//public class PlayerExtensionsTests
//{
//    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");

//    [Fact]
//    [Trait("Extension", "Player")]
//    public void Extension_Player_ShouldAttachUser()
//    {
//        // Arrange
//        PlayerEntity player = new();
//        User user = new() { Id = MOCK_USER_ID };

//        // Act
//        PlayerEntity updatedPlayer = player.SetUser(user);

//        // Assert
//        Assert.NotNull(updatedPlayer);
//        Assert.NotNull(player.User);
//        Assert.Equal(MOCK_USER_ID, player.User.Id);
//    }

//    [Fact]
//    [Trait("Extension", "Player")]
//    public void Extension_Player_ShouldAttachTypeToPlayerStats()
//    {
//        // Arrange
//        StatType statType = new() { Id = 10, Title = "Test" };
//        List<StatType> statTypes = new() { statType };
//        PlayerEntity player = new();

//        // Act
//        player.Stats.Add(new PlayerStat
//        {
//            Id = 0,
//            Type = new StatType { Id = 10 }
//        });
//        PlayerEntity updatedPlayer = player.SetStatTypes(statTypes);
//        PlayerStat? playerStat = player.Stats.FirstOrDefault(st => st.Type.Id == 10);

//        // Assert
//        Assert.NotNull(updatedPlayer);
//        Assert.NotNull(playerStat);
//        Assert.Equal(statType, playerStat.Type);
//    }
//}
