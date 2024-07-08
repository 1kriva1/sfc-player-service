using MassTransit;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Infrastructure.Authorization.OwnPlayer;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Services;
using SFC.Player.Infrastructure.Services.Hosted;

namespace SFC.Player.Infrastructure;
public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddRedis(builder.Configuration);

        builder.Services.AddMassTransit(builder.Configuration);

        builder.Services.AddCache(builder.Configuration);       

        builder.Services.AddSingleton<IUriService>(o =>
        {
            IHttpContextAccessor accessor = o.GetRequiredService<IHttpContextAccessor>();
            HttpRequest request = accessor.HttpContext!.Request;
            return new UriService(string.Concat(request.Scheme, "://", request.Host.ToUriComponent()));
        });

        // custom services
        builder.Services.AddTransient<IDateTimeService, DateTimeService>();

        // hosted services
        builder.Services.AddHostedService<DatabaseResetHostedService>();
        builder.Services.AddHostedService<DataInitializationHostedService>();

        // authorization
        builder.Services.AddScoped<IAuthorizationHandler, OwnPlayerHandler>();
    }
}
