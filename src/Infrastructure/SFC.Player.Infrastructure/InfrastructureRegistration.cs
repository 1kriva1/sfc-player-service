using System.Reflection;

using MassTransit;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Metadata;
using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Application.Interfaces.Reference;
using SFC.Player.Infrastructure.Authorization.OwnPlayer;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Extensions.Grpc;
using SFC.Player.Infrastructure.Services;
using SFC.Player.Infrastructure.Services.Common;
using SFC.Player.Infrastructure.Services.Hosted;
using SFC.Player.Infrastructure.Services.Identity;
using SFC.Player.Infrastructure.Services.Metadata;
using SFC.Player.Infrastructure.Services.Player;
using SFC.Player.Infrastructure.Services.Reference;

namespace SFC.Player.Infrastructure;
public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddHttpContextAccessor();        

        builder.Services.AddAccessTokenManagement();

        builder.Services.AddRedis(builder.Configuration);

        builder.AddMassTransit();

        builder.Services.AddCache(builder.Configuration);

        builder.Services.AddGrpc(builder.Configuration, builder.Environment);

        builder.Services.AddSingleton<IUriService>(o =>
        {
            IHttpContextAccessor accessor = o.GetRequiredService<IHttpContextAccessor>();
            HttpRequest request = accessor.HttpContext!.Request;
            return new UriService(string.Concat(request.Scheme, "://", request.Host.ToUriComponent()));
        });

        // custom services
        builder.Services.AddTransient<IMetadataService, MetadataService>();
        builder.Services.AddTransient<IDateTimeService, DateTimeService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IUserSeedService, UserSeedService>();
        builder.Services.AddTransient<IPlayerService, PlayerService>();
        builder.Services.AddTransient<IPlayerSeedService, PlayerSeedService>();

        // grpc
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        // references
        builder.Services.AddScoped<IIdentityReference, IdentityReference>();

        // hosted services
        builder.Services.AddHostedService<DatabaseResetHostedService>();
        builder.Services.AddHostedService<DataInitializationHostedService>();

        // authorization
        builder.Services.AddScoped<IAuthorizationHandler, OwnPlayerHandler>();        
    }
}
