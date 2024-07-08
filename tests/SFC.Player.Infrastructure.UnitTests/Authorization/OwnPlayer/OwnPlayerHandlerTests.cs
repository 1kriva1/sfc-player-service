using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Moq;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Infrastructure.Authorization.OwnPlayer;

namespace SFC.Player.Infrastructure.UnitTests.Authorization.OwnPlayer;
public class OwnPlayerHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository = new();

    [Fact]
    [Trait("Authorization", "OwnPlayer")]
    public async Task Authorization_OwnPlayer_ShouldHandlerReturnFailWhenHttpContextDoesNotHaveIdParameterInRoute()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        Mock<IHttpContextAccessor> httpContextAccessor  = BuildHttpContextAccessorMock(httpContext);
        OwnPlayerHandler handler = new(_mockUserRepository.Object, httpContextAccessor.Object);
        AuthorizationHandlerContext authorizationContext = new([new OwnPlayerRequirement()], new ClaimsPrincipal(), null);

        // Act
        await handler.HandleAsync(authorizationContext);

        // Assert
        Assert.False(authorizationContext.HasSucceeded);
        Assert.True(authorizationContext.HasFailed);
        Assert.Single(authorizationContext.FailureReasons);
        Assert.Equal("Route does not have \"id\" parameter value.", authorizationContext.FailureReasons.First().Message);
    }

    [Fact]
    [Trait("Authorization", "OwnPlayer")]
    public async Task Authorization_OwnPlayer_ShouldHandlerReturnFailWhenUserDoesNotHaveNameClaim()
    {
        // Arrange
        DefaultHttpContext httpContext = new();
        httpContext.Request.RouteValues = new RouteValueDictionary() { { "id", "1" } };
        Mock<IHttpContextAccessor> httpContextAccessor = BuildHttpContextAccessorMock(httpContext);
        OwnPlayerHandler handler = new(_mockUserRepository.Object, httpContextAccessor.Object);
        AuthorizationHandlerContext authorizationContext = new([new OwnPlayerRequirement()], new ClaimsPrincipal(), null);

        // Act
        await handler.HandleAsync(authorizationContext);

        // Assert
        Assert.False(authorizationContext.HasSucceeded);
        Assert.True(authorizationContext.HasFailed);
        Assert.Equal("User does not have NameIdentifier claim value.", authorizationContext.FailureReasons.First().Message);
    }

    [Fact]
    [Trait("Authorization", "OwnPlayer")]
    public async Task Authorization_OwnPlayer_ShouldHandlerReturnFailWhenUserDoesNotRelatedToResource()
    {
        // Arrange
        string resoureIdValue = "1";
        Guid nameIdentifierClaimValue = Guid.NewGuid();
        ClaimsPrincipal principal = new(new ClaimsIdentity(new List<Claim> {
                new(ClaimTypes.NameIdentifier, nameIdentifierClaimValue.ToString())
        }));
        DefaultHttpContext httpContext = new();
        httpContext.Request.RouteValues = new RouteValueDictionary() { { "id", resoureIdValue } };
        httpContext.User = principal;
        Mock<IHttpContextAccessor> httpContextAccessor = BuildHttpContextAccessorMock(httpContext);
        _mockUserRepository.Setup(r => r.AnyAsync(long.Parse(resoureIdValue), nameIdentifierClaimValue)).ReturnsAsync(false);
        OwnPlayerHandler handler = new(_mockUserRepository.Object, httpContextAccessor.Object);
        AuthorizationHandlerContext authorizationContext = new([new OwnPlayerRequirement()], principal, null);

        // Act
        await handler.HandleAsync(authorizationContext);

        // Assert
        Assert.False(authorizationContext.HasSucceeded);
        Assert.True(authorizationContext.HasFailed);
        Assert.Equal($"User - {nameIdentifierClaimValue} does not related to this resource - {resoureIdValue}.",
            authorizationContext.FailureReasons.First().Message);
    }

    [Fact]
    [Trait("Authorization", "OwnPlayer")]
    public async Task Authorization_OwnPlayer_ShouldHandlerReturnSucceed()
    {
        // Arrange
        string resoureIdValue = "1";
        Guid nameIdentifierClaimValue = Guid.NewGuid();
        ClaimsPrincipal principal = new(new ClaimsIdentity(new List<Claim> {
                new(ClaimTypes.NameIdentifier, nameIdentifierClaimValue.ToString())
        }));
        DefaultHttpContext httpContext = new();
        httpContext.Request.RouteValues = new RouteValueDictionary() { { "id", resoureIdValue } };
        httpContext.User = principal;
        Mock<IHttpContextAccessor> httpContextAccessor = BuildHttpContextAccessorMock(httpContext);
        _mockUserRepository.Setup(r => r.AnyAsync(long.Parse(resoureIdValue), nameIdentifierClaimValue)).ReturnsAsync(true);
        OwnPlayerHandler handler = new(_mockUserRepository.Object, httpContextAccessor.Object);
        AuthorizationHandlerContext authorizationContext = new([new OwnPlayerRequirement()], principal, null);

        // Act
        await handler.HandleAsync(authorizationContext);

        // Assert
        Assert.True(authorizationContext.HasSucceeded);
        Assert.False(authorizationContext.HasFailed);
        Assert.Empty(authorizationContext.FailureReasons);
    }

    private static Mock<IHttpContextAccessor> BuildHttpContextAccessorMock(DefaultHttpContext httpContext)
    {
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock.Setup(m => m.HttpContext).Returns(httpContext);
        return httpContextAccessorMock;
    }
}
