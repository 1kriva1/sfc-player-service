using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Behaviours;
using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Models;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Application.Models.Players.Create;

namespace SFC.Players.Application.UnitTests.Common.Behaviours;
public class LoggingBehaviourTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly Mock<ILogger<LoggingBehaviour<CreatePlayerCommand, BaseResponse>>> _loggerMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();

    public LoggingBehaviourTests()
    {
        _userServiceMock.Setup(us => us.UserId).Returns(MOCK_USER_ID);
    }

    [Fact]
    [Trait("Behaviour", "Logging")]
    public async Task Behaviour_Logging_ShouldLogInputOutputRequestInformation()
    {
        // Arrange
        CreatePlayerCommand request = new();
        LoggingBehaviour<CreatePlayerCommand, BaseResponse> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

        // Act
        BaseResponse response = await requestLogger.Handle(request, () => Task.FromResult(new BaseResponse()), new CancellationToken());

        // Assert
        VerifyLogMessage($"Handling {typeof(CreatePlayerCommand).Name} for user {MOCK_USER_ID}.", (int)RequestId.CreatePlayer);
        VerifyLogMessage($"Handled {typeof(BaseResponse).Name} for user {MOCK_USER_ID}.", (int)RequestId.CreatePlayer);
    }

    [Fact]
    [Trait("Behaviour", "Logging")]
    public async Task Behaviour_Logging_ShouldLogRequestValue()
    {
        // Arrange
        CreatePlayerCommand request = new()
        {
            Player = new CreatePlayerDto
            {
                Profile = new PlayerProfileDto { General = new PlayerGeneralProfileDto { FirstName = "Testname" } }
            },
            UserId = MOCK_USER_ID
        };
        LoggingBehaviour<CreatePlayerCommand, BaseResponse> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

        // Act
        BaseResponse response = await requestLogger.Handle(request, () => Task.FromResult(new BaseResponse()), new CancellationToken());

        // Assert
        VerifyLogMessage("Request: {\"RequestId\":0,\"Player\":{\"Profile\":{\"General\":{\"FirstName\":\"Testname\",\"LastName\":null," +
            "\"Photo\":null,\"Biography\":null,\"Birthday\":null,\"City\":null,\"FreePlay\":false,\"Tags\":[],\"Availability\":null}," +
            "\"Football\":null},\"Stats\":null},\"EventId\":{\"Id\":0,\"Name\":\"CreatePlayer\"},\"UserId\":\"db69fc8c-cd50-4c99-96b3-9ddb6c49d08b\"}",
            (int)RequestId.CreatePlayer,
            LogLevel.Debug);
    }

    [Fact]
    [Trait("Behaviour", "Logging")]
    public async Task Behaviour_Logging_ShouldReturnResponse()
    {
        // Arrange
        CreatePlayerCommand request = new();
        LoggingBehaviour<CreatePlayerCommand, BaseResponse> requestLogger = new(_loggerMock.Object, _userServiceMock.Object);

        // Act
        BaseResponse response = await requestLogger.Handle(request, () => Task.FromResult(new BaseResponse()), new CancellationToken());

        // Assert
       Assert.NotNull(response);
    }

    private void VerifyLogMessage(string message, int id, LogLevel level = LogLevel.Information)
    {
        _loggerMock.Verify(logger => logger.Log(
           It.Is<LogLevel>(logLevel => logLevel == level),
           It.Is<EventId>(eventId => eventId.Id == id),
           It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == message),
           It.IsAny<Exception>(),
           It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
    }
}