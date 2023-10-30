using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Infrastructure.Services.Hosted;

namespace SFC.Players.Infrastructure.UnitTests;
public class InfrastructureRegistrationTests
{
    private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();

    [Fact]
    [Trait("Registration", "Custom Services")]
    public void InfrastructureRegistration_Execute_CustomServicesAreRegistered()
    {
        // Arrange
        _builder.AddInfrastructureServices();
        using WebApplication application = _builder.Build();

        // Assert
        Assert.NotNull(application.Services.GetService<IDateTimeService>());
        Assert.NotNull(_builder.Services.FirstOrDefault(s => s.ImplementationType == typeof(DataInitializationHostedService)));
        Assert.NotNull(_builder.Services.FirstOrDefault(s => s.ImplementationType == typeof(DatabaseResetHostedService)));
    }
}
