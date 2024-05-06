using SFC.Player.Api.Middlewares;

namespace SFC.Player.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }    
}
