﻿using SFC.Players.Application.Common.Exceptions;

namespace SFC.Players.Application.UnitTests.Common.Exceptions;
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
