//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Common.Base;

//namespace SFC.Player.Application.UnitTests.Features.Common.Base;
//public class BasePaginationRequestTests
//{
//    public class TestBasePaginationRequest : BasePaginationRequest<int, string>
//    {
//        public override RequestId RequestId => throw new NotImplementedException();
//    }

//    [Fact]
//    [Trait("Request", "Pagination")]
//    public void Request_Pagination_ShouldSetRoute()
//    {
//        // Arrange
//        string assertRoute = "route";
//        TestBasePaginationRequest request = new();

//        // Act
//        BasePaginationRequest<int, string> updatedRequest = request.SetRoute(assertRoute);

//        // Assert
//        Assert.NotNull(updatedRequest);
//        Assert.Equal(assertRoute, updatedRequest.Route);
//    }

//    [Fact]
//    [Trait("Request", "Pagination")]
//    public void Request_Pagination_ShouldSetQueryString()
//    {
//        // Arrange
//        string assertQueryString = "queryString";
//        TestBasePaginationRequest request = new();

//        // Act
//        BasePaginationRequest<int, string> updatedRequest = request.SetQueryString(assertQueryString);

//        // Assert
//        Assert.NotNull(updatedRequest);
//        Assert.Equal(assertQueryString, updatedRequest.QueryString);
//    }
//}
