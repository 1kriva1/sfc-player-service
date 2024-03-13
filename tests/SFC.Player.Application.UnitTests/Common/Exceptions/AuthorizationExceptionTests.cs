using SFC.Player.Application.Common.Exceptions;

namespace SFC.Player.Application.UnitTests.Common.Exceptions;
public class AuthorizationExceptionTests
{
    [Fact]
    [Trait("Exception", "Authorization")]
    public void Exception_Authorization_ShouldHaveDefinedMessage()
    {
        // Arrange
        string validationMessage = "Test validation message.";

        // Act
        AuthorizationException exception = new(validationMessage);

        // Assert
        Assert.Equal(validationMessage, exception.Message);
    }
}
