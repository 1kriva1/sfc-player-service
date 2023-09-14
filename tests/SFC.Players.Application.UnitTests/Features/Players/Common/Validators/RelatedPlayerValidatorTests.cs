using FluentValidation.Results;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Common.Models;
using SFC.Players.Application.Features.Players.Common.Validators;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Application.UnitTests.Features.Players.Common.Validators;
public class RelatedPlayerValidatorTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly Mock<IUserRepository> _mockUserRepository = new();

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_RelatedPlayer_ShouldFailValidationWhenUserNotRelatedToPlayer()
    {
        // Arrange
        IPlayerRelatedRequest request = new GetPlayerQuery
        {
            PlayerId = 1,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(request.PlayerId, request.UserId)).ReturnsAsync(false);

        RelatedPlayerValidator validator = new(_mockUserRepository.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(Messages.PlayerNotRelatedToThisUser, failure.ErrorMessage);
        Assert.Equal(nameof(IPlayerRelatedRequest.PlayerId), failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "Validators")]
    public async Task Feature_Validator_RelatedPlayer_ShouldPassValidationWhenUserRelatedToPlayer()
    {
        // Arrange
        IPlayerRelatedRequest request = new GetPlayerQuery
        {
            PlayerId = 1,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(request.PlayerId, request.UserId)).ReturnsAsync(true);

        RelatedPlayerValidator validator = new(_mockUserRepository.Object);

        // Act
        ValidationResult result = await validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
