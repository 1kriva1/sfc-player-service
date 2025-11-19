using SFC.Player.Api.Services;

namespace SFC.Player.Api.Infrastructure.Extensions;

public static class GrpcExtensions
{
    public static WebApplication UseGrpc(this WebApplication app)
    {
        app.MapGrpcService<PlayerService>();
        return app;
    }
}