//using MassTransit;

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Infrastructure.Services.Hosted;

//namespace SFC.Player.Infrastructure.UnitTests.Services.Hosted;
//public class DataInitializationHostedServiceTests
//{
//    //private readonly Mock<ILogger<DataInitializationHostedService>> _loggerMock = new();

//    //[Fact]
//    //[Trait("Service", "DataInitializationHosted")]
//    //public async Task Service_Hosted_DataInitialization_ShouldPublishDataRequireEvent()
//    //{
//    //    // Arrange
//    //    IServiceCollection services = new ServiceCollection();
//    //    Mock<IPublishEndpoint> publishMock = new();
//    //    DataRequireMessage @event = null!;
//    //    publishMock.Setup(p => p.Publish(It.IsAny<DataRequireMessage>(), It.IsAny<CancellationToken>()))
//    //        .Callback<DataRequireMessage, CancellationToken>((assertEvent, _) => @event = assertEvent);
//    //    services.AddSingleton(publishMock.Object);

//    //    IHostedService service = new DataInitializationHostedService(_loggerMock.Object, services.BuildServiceProvider());

//    //    // Act
//    //    await service.StartAsync(new CancellationToken());

//    //    // Assert
//    //    publishMock.Verify(mock => mock.Publish(It.IsAny<DataRequireMessage>(), It.IsAny<CancellationToken>()), Times.Once());
//    //    Assert.Equal(DataInitiator.Player, @event!.Initiator);
//    //}
//}