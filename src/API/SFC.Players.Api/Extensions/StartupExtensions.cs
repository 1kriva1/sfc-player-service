using Microsoft.AspNetCore.Mvc;
using SFC.Players.Application;
using SFC.Players.Infrastructure.Persistence;
using SFC.Players.Infrastructure;
using SFC.Players.Application.Common.Constants;

namespace SFC.Players.Api.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();

        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.AddInfrastructureServices();

        builder.AddApiServices();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<MvcOptions>(options => options.AllowEmptyInputInBodyModelBinding = true);

        builder.Services.AddCors();

        builder.AddControllers();

        builder.AddLocalization();

        builder.AddAuthentication();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(CommonConstants.PAGINATION_HEADER_KEY)
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());// allow credentials 

        app.UseHttpsRedirection();

        app.UseLocalization();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCustomExceptionHandler();

        app.MapControllers();

        return app;
    }
}
