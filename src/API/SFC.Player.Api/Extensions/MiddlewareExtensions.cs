using Microsoft.Extensions.Options;
using SFC.Player.Application;
using SFC.Player.Application.Common.Constants;
using Microsoft.Extensions.Localization;
using SFC.Player.Api.Middlewares;

namespace SFC.Player.Api.Extensions;

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
