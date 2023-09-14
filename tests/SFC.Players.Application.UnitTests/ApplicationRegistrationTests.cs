using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using MediatR;

namespace SFC.Players.Application.UnitTests;
public class ApplicationRegistrationTests
{
    [Fact]
    [Trait("Registration", "Servises")]
    public void ApplicationRegistration_Execute_ServicesAreRegistered()
    {
        // Arrange
        ServiceCollection serviceCollection = new();

        // Act
        serviceCollection.AddApplicationServices();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IMediator>());
        Assert.NotNull(serviceProvider.GetService<IMapper>());
    }
}
