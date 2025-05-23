//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;

//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Infrastructure.Services.Hosted;

//namespace SFC.Player.Infrastructure.UnitTests;
//public class InfrastructureRegistrationTests
//{
//    private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();
//    private static DefaultHttpContext _httpContext = new() { User = new() };

//    public class TestHttpContextAccessor : IHttpContextAccessor
//    {
//        public HttpContext? HttpContext { get => _httpContext; set => _httpContext = new() { User = new() }; }
//    }

//    [Fact]
//    [Trait("Registration", "Custom Services")]
//    public void InfrastructureRegistration_Execute_CustomServicesAreRegistered()
//    {
//        // Arrange
//        _builder.Services.AddTransient<IHttpContextAccessor, TestHttpContextAccessor>();
//        _builder.AddInfrastructureServices();
//        using WebApplication application = _builder.Build();

//        // Assert
//        Assert.NotNull(application.Services.GetService<IDateTimeService>());
//        Assert.NotNull(application.Services.GetService<IUriService>());
//        Assert.NotNull(_builder.Services.FirstOrDefault(s => s.ImplementationType == typeof(DataInitializationHostedService)));
//        Assert.NotNull(_builder.Services.FirstOrDefault(s => s.ImplementationType == typeof(DatabaseResetHostedService)));
//    }
//}
