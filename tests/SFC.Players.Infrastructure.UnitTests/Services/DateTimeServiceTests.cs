using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Infrastructure.Services;

namespace SFC.Players.Infrastructure.UnitTests.Services;
public class DateTimeServiceTests
{
    [Fact]
    [Trait("Service", "DateTime")]
    public void Service_DateTime_ShouldReturnCurrentUtcValue()
    {
        // Arrange
        IDateTimeService service = new DateTimeService();

        // Act
        DateTime result = service.Now;

        // Assert
        Assert.Equal(DateTime.UtcNow.Date, result.Date);
    }

    [Fact]
    [Trait("Service", "DateTime")]
    public void Service_DateTime_ShouldReturnCurrentUtcDateValue()
    {
        // Arrange
        IDateTimeService service = new DateTimeService();

        // Act
        DateTime result = service.DateNow;

        // Assert
        Assert.Equal(DateTime.UtcNow.Date, result.Date);
    }
}