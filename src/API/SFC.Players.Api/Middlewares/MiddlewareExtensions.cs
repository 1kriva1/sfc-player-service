using Microsoft.Extensions.Options;
using SFC.Players.Application;
using SFC.Players.Application.Common.Constants;
using SFC.Players.Api.Middlewares.Exception;
using Microsoft.Extensions.Localization;

namespace SFC.Players.Api.Middlewares;

public static class MiddlewareExtensions
{
    public static void UseLocalization(this WebApplication app)
    {
        IOptions<RequestLocalizationOptions> localizationOptions =
            app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;

        app.UseRequestLocalization(localizationOptions.Value);

        // inject localizer explicity for error messages
        IStringLocalizer<Resources> localizer =
            app.Services.GetService<IStringLocalizer<Resources>>()!;

        Messages.Configure(localizer);
    }

    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
