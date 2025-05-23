//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Application.Common.Behaviours;
//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Players.Commands.Create;
//using SFC.Player.Application.Features.Players.Common.Dto;
//using SFC.Player.Application.Interfaces.Identity;

//namespace SFC.Player.Application.UnitTests.Common.Behaviours;
//public class LoggingBehaviourTests
//{
//    private readonly Guid _userIdMock = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
//    private readonly Mock<ILogger<LoggingBehaviour<CreatePlayerCommand, CreatePlayerViewModel>>> _loggerMock = new();
//    private readonly Mock<IUserService> _userServiceMock = new();

//    [Fact]
//    [Trait("Behaviour", "Logging")]
//    public async Task Behaviour_Logging_ShouldLogInputOutputRequestInformation()
//    {
//        // Arrange
//        CreatePlayerCommand request = new();
//        LoggingBehaviour<CreatePlayerCommand, CreatePlayerViewModel> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

//        // Act
//        CreatePlayerViewModel response = await requestLogger.Handle(request, () => Task.FromResult(new CreatePlayerViewModel()), new CancellationToken());

//        // Assert
//        VerifyLogMessage($"Handling {typeof(CreatePlayerCommand).Name} for user {_userIdMock}.", (int)RequestId.CreatePlayer);
//        VerifyLogMessage($"Handled {typeof(CreatePlayerViewModel).Name} for user {_userIdMock}.", (int)RequestId.CreatePlayer);
//    }

//    [Fact]
//    [Trait("Behaviour", "Logging")]
//    public async Task Behaviour_Logging_ShouldLogRequestValue()
//    {
//        // Arrange
//        CreatePlayerCommand request = new()
//        {
//            Player = new CreatePlayerDto
//            {
//                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = "Testname" } }
//            }
//        };
//        LoggingBehaviour<CreatePlayerCommand, CreatePlayerViewModel> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

//        // Act
//        CreatePlayerViewModel response = await requestLogger.Handle(request, () => Task.FromResult(new CreatePlayerViewModel()), new CancellationToken());

//        // Assert
//        VerifyLogMessage("Request: {\"RequestId\":0,\"Player\":{\"Profile\":{\"General\":{\"FirstName\":\"Testname\",\"LastName\":null," +
//            "\"Photo\":null,\"Biography\":null,\"Birthday\":null,\"City\":null,\"FreePlay\":false,\"Tags\":[],\"Availability\":null}," +
//            "\"Football\":null},\"Stats\":null},\"EventId\":{\"Id\":0,\"Name\":\"CreatePlayer\"},\"UserId\":\"db69fc8c-cd50-4c99-96b3-9ddb6c49d08b\"}",
//            (int)RequestId.CreatePlayer,
//            LogLevel.Debug);
//    }

//    [Fact]
//    [Trait("Behaviour", "Logging")]
//    public async Task Behaviour_Logging_ShouldReturnResponse()
//    {
//        // Arrange
//        CreatePlayerCommand request = new();
//        LoggingBehaviour<CreatePlayerCommand, CreatePlayerViewModel> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

//        // Act
//        CreatePlayerViewModel response = await requestLogger.Handle(request, () => Task.FromResult(new CreatePlayerViewModel()), new CancellationToken());

//        // Assert
//       Assert.NotNull(response);
//    }

//    private void VerifyLogMessage(string message, int id, LogLevel level = LogLevel.Information)
//    {
//        _loggerMock.Verify(logger => logger.Log(
//           It.Is<LogLevel>(logLevel => logLevel == level),
//           It.Is<EventId>(eventId => eventId.Id == id),
//           It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == message),
//           It.IsAny<Exception>(),
//           It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
//    }
//}