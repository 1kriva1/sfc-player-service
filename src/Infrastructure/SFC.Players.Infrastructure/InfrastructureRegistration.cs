using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Infrastructure.Services;

namespace SFC.Players.Infrastructure;
public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
    }
}
