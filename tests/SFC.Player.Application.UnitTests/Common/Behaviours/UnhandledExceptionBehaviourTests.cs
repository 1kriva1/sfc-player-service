//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Application.Common.Behaviours;
//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Players.Queries.Get;

//namespace SFC.Player.Application.UnitTests.Common.Behaviours;
//public class UnhandledExceptionBehaviourTests
//{
//    private readonly Mock<ILogger<GetPlayerQuery>> _loggerMock = new();

//    [Fact]
//    [Trait("Behaviour", "Unhandled exception")]
//    public async Task Behaviour_UnhandledException_ShouldReturnResponse()
//    {
//        // Arrange
//        GetPlayerQuery request = new();
//        UnhandledExceptionBehaviour<GetPlayerQuery, GetPlayerViewModel> requestUnhandledException = new(_loggerMock.Object);

//        // Act
//        GetPlayerViewModel response = await requestUnhandledException.Handle(request, () => Task.FromResult(new GetPlayerViewModel()), new CancellationToken());

//        // Assert
//        Assert.NotNull(response);
//    }

//    [Fact]
//    [Trait("Behaviour", "Unhandled exception")]
//    public async Task Behaviour_UnhandledException_ShouldThrowException()
//    {
//        // Arrange
//        GetPlayerQuery request = new();
//        Exception exception = new("Test error.");
//        UnhandledExceptionBehaviour<GetPlayerQuery, GetPlayerViewModel> requestUnhandledException = new(_loggerMock.Object);

//        // Act
//        Exception assertException = await Assert.ThrowsAsync<Exception>(async () => 
//            await requestUnhandledException.Handle(request, () => throw exception, new CancellationToken()));

//        // Assert
//        Assert.NotNull(assertException);
//        Assert.Equal(exception.Message, assertException.Message);
//    }

//    [Fact]
//    [Trait("Behaviour", "Unhandled exception")]
//    public async Task Behaviour_UnhandledException_ShouldLogException()
//    {

//        // Arrange
//        GetPlayerQuery request = new();
//        Exception exception = new("Test error.");
//        UnhandledExceptionBehaviour<GetPlayerQuery, GetPlayerViewModel> requestUnhandledException = new(_loggerMock.Object);

//        // Act
//        Exception assertException = await Assert.ThrowsAsync<Exception>(async () =>
//            await requestUnhandledException.Handle(request, () => throw exception, new CancellationToken()));

//        // Assert
//        _loggerMock.Verify(logger => logger.Log(
//          It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
//          It.Is<EventId>(eventId => eventId.Id == (int)RequestId.GetPlayer),
//          It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == $"Unhandled Exception for {typeof(GetPlayerQuery).Name}"),
//          It.Is<Exception>(ex => ex == exception),
//          It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
//    }
//}


