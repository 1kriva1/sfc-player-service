using SFC.Player.Application;
using SFC.Player.Infrastructure;
using SFC.Player.Infrastructure.Constants;
using SFC.Player.Infrastructure.Persistence;

namespace SFC.Player.Api.Infrastructure.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();

        builder.AddPersistenceServices();

        builder.AddInfrastructureServices();

        builder.Services.AddApiServices();

        builder.Services.AddControllers();

        builder.Services.AddLocalization();

        builder.AddAuthentication();

        if (builder.Environment.IsDevelopment())
        {
            builder.AddSwagger();
        }

        builder.Services.AddHealthChecks();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(CommonConstants.PaginationHeaderKey)
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());// allow credentials

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
        }

        app.UseHttpsRedirection();

        app.UseLocalization();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCustomExceptionHandler();

        app.MapHealthChecks("/health");

        app.MapControllers();

        app.UseGrpc();

        return app;
    }
}