using FluentValidation.Results;

using Moq;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Features.Player.Common.Models;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Interfaces.Persistence;

namespace SFC.Player.Application.UnitTests.Features.Player.Queries.Get;
public class GetPlayerQueryValidatorTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly Mock<IUserRepository> _mockUserRepository = new();

    [Fact]
    [Trait("Feature", "GetPlayer")]
    public async Task Feature_GetPlayer_ShouldFailValidationWhenPlayerNotRelatedForUser()
    {
        // Arrange
        GetPlayerQuery query = new()
        {
            PlayerId = 1,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(query.PlayerId, query.UserId)).ReturnsAsync(false);

        GetPlayerQueryValidator validator = new(_mockUserRepository.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(query);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(Messages.PlayerNotRelatedToThisUser, failure.ErrorMessage);
        Assert.Equal(nameof(IPlayerRelatedRequest.PlayerId), failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "GetPlayer")]
    public async Task Feature_GetPlayer_ShouldPassValidationWhenPlayerExistForUser()
    {
        // Arrange
        GetPlayerQuery query = new()
        {
            PlayerId = 1,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(query.PlayerId, query.UserId)).ReturnsAsync(true);

        GetPlayerQueryValidator validator = new(_mockUserRepository.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(query);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
