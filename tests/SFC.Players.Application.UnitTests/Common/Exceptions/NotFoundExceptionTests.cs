using SFC.Players.Application.Common.Exceptions;

namespace SFC.Players.Application.UnitTests.Common.Exceptions;
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
