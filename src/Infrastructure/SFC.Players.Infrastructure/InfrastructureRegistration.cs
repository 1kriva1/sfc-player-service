using MassTransit;

using Microsoft.AspNetCore.Builder;
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

        builder.Services.AddHostedService<DatabaseResetHostedService>();

        builder.Services.AddHostedService<DataInitializationHostedService>();
    }
}
