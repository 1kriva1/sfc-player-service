using SFC.Player.Application.Common.Exceptions;

namespace SFC.Player.Application.UnitTests.Common.Exceptions;
public class NotFoundExceptionTests
{
    [Fact]
    [Trait("Exception", "NotFound")]
    public void Exception_NotFound_ShouldHaveDefinedMessage()
    {
        // Arrange
        string validationMessage = "Test validation message.";

        // Act
        NotFoundException exception = new(validationMessage);

        // Assert
        Assert.Equal(validationMessage, exception.Message);
    }
}
