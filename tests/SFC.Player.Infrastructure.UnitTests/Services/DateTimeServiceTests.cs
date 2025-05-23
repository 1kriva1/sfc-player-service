//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Infrastructure.Services;

//namespace SFC.Player.Infrastructure.UnitTests.Services;
//public class DateTimeServiceTests
//{
//    [Fact]
//    [Trait("Service", "DateTime")]
//    public void Service_DateTime_ShouldReturnCurrentUtcValue()
//    {
//        // Arrange
//        IDateTimeService service = new DateTimeService();

//        // Act
//        DateTime result = service.Now;

//        // Assert
//        Assert.Equal(DateTime.UtcNow.Date, result.Date);
//    }

//    [Fact]
//    [Trait("Service", "DateTime")]
//    public void Service_DateTime_ShouldReturnCurrentUtcDateValue()
//    {
//        // Arrange
//        IDateTimeService service = new DateTimeService();

//        // Act
//        DateTime result = service.DateNow;

//        // Assert
//        Assert.Equal(DateTime.UtcNow.Date, result.Date);
//    }
//}