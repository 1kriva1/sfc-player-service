using FluentValidation.Results;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Common.Models;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Application.UnitTests.Features.Players.Queries.Get;
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
