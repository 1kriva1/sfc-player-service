using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Infrastructure.Extensions;
using SFC.Players.Infrastructure.Services;
using SFC.Players.Infrastructure.Services.Hosted;

namespace SFC.Players.Infrastructure;
public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(builder.Configuration);

        builder.Services.AddTransient<IDateTimeService, DateTimeService>();

        builder.Services.AddSingleton<IUriService>(o =>
        {
            IHttpContextAccessor accessor = o.GetRequiredService<IHttpContextAccessor>();
            HttpRequest request = accessor.HttpContext!.Request;
            return new UriService(string.Concat(request.Scheme, "://", request.Host.ToUriComponent()));
        });

        builder.Services.AddHostedService<DatabaseResetHostedService>();

        builder.Services.AddHostedService<DataInitializationHostedService>();
    }
}
