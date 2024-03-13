using FluentValidation;
using FluentValidation.Results;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Application.Features.Player.Create;

namespace SFC.Player.Application.UnitTests.Common.Extensions;
public class ValidationExtensionsTests
{
    public class TestValidator : AbstractValidator<CreatePlayerCommand>
    {
        public TestValidator(int? maxLength = null, string? propName = null)
        {
            RuleFor(p => p.Player.Profile.General.FirstName)
                .RequiredProperty(maxLength, propName);
        }
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Validation_ShouldBeNotValidWithCustomName()
    {
        // Arrange
        string propertyName = "TestName";
        int maxLength = 3;
        string firstName = "1234";
        TestValidator validator = new(maxLength, propertyName);
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto
            {
                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = firstName } }
            }
        };

        // Act
        ValidationResult result = validator.Validate(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal($"The length of '{propertyName}' must be {maxLength} characters or fewer. You entered {firstName.Length} characters.",
            result.Errors.First().ErrorMessage);
    }


    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Validation_ShouldBeNotValidWithoutCustomName()
    {
        // Arrange
        int maxLength = 3;
        string firstName = "1234";
        TestValidator validator = new(maxLength);
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto
            {
                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = firstName } }
            }
        };

        // Act
        ValidationResult result = validator.Validate(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal($"The length of 'Player Profile General First Name' must be {maxLength} characters or fewer. You entered {firstName.Length} characters.",
            result.Errors.First().ErrorMessage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Validation_ShouldBeNotValidWhenEmptyWithCustomName()
    {
        // Arrange
        string propertyName = "TestName";
        string firstName = string.Empty;
        TestValidator validator = new(null, propertyName);
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto
            {
                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = firstName } }
            }
        };

        // Act
        ValidationResult result = validator.Validate(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal($"'{propertyName}' must not be empty.",
           result.Errors.First().ErrorMessage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Validation_ShouldBeValid()
    {
        // Arrange
        string firstName = "FirstName";
        TestValidator validator = new();
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto
            {
                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = firstName } }
            }
        };

        // Act
        ValidationResult result = validator.Validate(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
