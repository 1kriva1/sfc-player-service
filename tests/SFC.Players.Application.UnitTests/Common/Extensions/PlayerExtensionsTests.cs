using SFC.Players.Application.Common.Extensions;
using SFC.Players.Domain.Entities;

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
}
