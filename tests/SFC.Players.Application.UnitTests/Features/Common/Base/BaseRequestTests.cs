using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Features.Common.Base;

namespace SFC.Players.Application.UnitTests.Features.Common.Base;
public class BaseRequestTests
{
    public class TestBaseRequest : BaseRequest
    {
        public override RequestId RequestId => RequestId.GetPlayersByFilters;
    }

    [Fact]
    [Trait("Request", "Pagination")]
    public void Request_Base_ShouldSetUserId()
    {
        // Arrange
        Guid assertUserId = Guid.NewGuid();
        TestBaseRequest request = new();

        // Act
        TestBaseRequest updatedRequest = request.SetUserId<TestBaseRequest>(assertUserId);

        // Assert
        Assert.NotNull(updatedRequest);
        Assert.Equal(assertUserId, updatedRequest.UserId);
    }

    [Fact]
    [Trait("Request", "Pagination")]
    public void Request_Base_ShouldBuildEventId()
    {
        // Arrange
        TestBaseRequest request = new();

        // Assert
        Assert.Equal(new(4, "GetPlayersByFilters"), request.EventId);
    }
}
