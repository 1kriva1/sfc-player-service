using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Behaviours;
using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Models;
using SFC.Players.Application.Features.Players.Commands.Update;

namespace SFC.Players.Application.UnitTests.Common.Behaviours;
public class PerformanceBehaviourTests
{
    private readonly Mock<ILogger<UpdatePlayerCommand>> _loggerMock = new();

    [Fact]
    [Trait("Behaviour", "Performance")]
    public async Task Behaviour_Performance_ShouldReturnResponse()
    {
        // Arrange
        UpdatePlayerCommand request = new();
        PerformanceBehaviour<UpdatePlayerCommand, BaseResponse> requestPerformance = new(_loggerMock.Object);

        // Act
        BaseResponse response = await requestPerformance.Handle(request, () => Task.FromResult(new BaseResponse()), new CancellationToken());

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    [Trait("Behaviour", "Performance")]
    public async Task Behaviour_Performance_ShouldLogExecutionTime()
    {
        // Arrange
        UpdatePlayerCommand request = new();
        PerformanceBehaviour<UpdatePlayerCommand, BaseResponse> requestPerformance = new(_loggerMock.Object);

        // Act
        BaseResponse response = await requestPerformance.Handle(request, () => Task.FromResult(new BaseResponse()), new CancellationToken());

        // Assert
        _loggerMock.Verify(logger => logger.Log(
          It.Is<LogLevel>(logLevel => logLevel == LogLevel.Debug),
          It.Is<EventId>(eventId => eventId.Id == (int)RequestId.UpdatePlayer),
          It.Is<It.IsAnyType>((@object, @type) => @object.ToString()!.Contains($"Execution time for {typeof(UpdatePlayerCommand).Name}")),
          It.IsAny<Exception>(),
          It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
    }
}
