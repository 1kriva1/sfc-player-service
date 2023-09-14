using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application.Interfaces.Common;

namespace SFC.Players.Infrastructure.UnitTests;
public class InfrastructureRegistrationTests
{
    private readonly ServiceProvider _serviceProvider;

    public InfrastructureRegistrationTests()
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddInfrastructureServices();
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    [Trait("Registration", "Custom Services")]
    public void InfrastructureRegistration_Execute_CustomServicesAreRegistered()
    {
        // Assert
        Assert.NotNull(_serviceProvider.GetService<IDateTimeService>());
    }
}
