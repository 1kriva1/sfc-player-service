using FluentValidation.Results;

using SFC.Player.Application.Common.Exceptions;

namespace SFC.Player.Application.UnitTests.Common.Exceptions;
public class BadRequestExceptionTests
{
    [Fact]
    [Trait("Exception", "BadRequest")]
    public void Exception_BadRequest_ShouldCreateDefinedErrors()
    {
        // Arrange
        string validationMessage = "Test validation message.";
        Dictionary<string, IEnumerable<string>> errors = new()
        {
            { "Key", new List<string>{ "Test error message."} }
        };

        // Act
        BadRequestException exception = new(validationMessage, errors);

        // Assert
        Assert.Equal(exception.Errors, errors);
    }

    [Fact]
    [Trait("Exception", "BadRequest")]
    public void Exception_BadRequest_ShouldCreateSingleError()
    {
        // Arrange
        string validationMessage = "Test validation message.";
        (string, string) singleError = ("Key", "Test error message.");

        // Act
        BadRequestException exception = new(validationMessage, singleError);

        // Assert
        Assert.True(exception.Errors.ContainsKey(singleError.Item1));
        Assert.Equal(singleError.Item2, exception.Errors[singleError.Item1].First());
    }

    [Fact]
    [Trait("Exception", "BadRequest")]
    public void Exception_BadRequest_ShouldSingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        // Arrange
        string validationMessage = "Test validation message.";
        List<ValidationFailure> failures = new() { new ValidationFailure("Key", "Test error message.") };

        // Act
        BadRequestException exception = new(validationMessage, failures);

        // Assert
        Assert.True(exception.Errors.ContainsKey("Key"));
        Assert.Equal("Test error message.", exception.Errors["Key"].First());
    }

    [Fact]
    [Trait("Exception", "BadRequest")]
    public void Exception_BadRequest_ShouldMulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        // Arrange
        string validationMessage = "Test validation message.";
        List<ValidationFailure> failures = new() {
                new ValidationFailure("Age", "must be 18 or older"),
                new ValidationFailure("Age", "must be 25 or younger"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain lower case letter"),
            };

        // Act
        BadRequestException exception = new(validationMessage, failures);

        // Assert
        Assert.Equal(2, exception.Errors.Count);
        Assert.Equal(new List<string> {
                "must be 18 or older",
                "must be 25 or younger"                
        }, exception.Errors["Age"]);
        Assert.Equal(new List<string> {
                "must contain at least 8 characters",
                "must contain a digit",
                "must contain upper case letter",
                "must contain lower case letter"
        }, exception.Errors["Password"]);
    }
}
