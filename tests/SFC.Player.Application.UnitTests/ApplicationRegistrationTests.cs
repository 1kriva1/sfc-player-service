using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using MediatR;
using SFC.Player.Application.Common.Behaviours;
using SFC.Player.Application.Features.Player.Commands.Create;

namespace SFC.Player.Application.UnitTests;
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
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(UnhandledExceptionBehaviour<,>)));
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(LoggingBehaviour<,>)));
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(PerformanceBehaviour<,>)));
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(ValidationBehaviour<,>)));
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(CreatePlayerCommandHandler)));
        Assert.NotNull(serviceCollection.FirstOrDefault(s => s.ImplementationType == typeof(CreatePlayerCommandValidator)));
    }
}
