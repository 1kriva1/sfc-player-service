using SFC.Players.Application.Models.Base;

namespace SFC.Players.Application.UnitTests.Common.Models.Base;
public class BaseListResponseTests
{
    [Fact]
    [Trait("Model", "BaseListResponse")]
    public void Model_BaseListResponse_ShouldHaveEmptyItemsByDefault()
    {
        // Arrange
        BaseListResponse<int> response = new();

        // Assert
        Assert.False(response.Items.Any());
    }
}
