using SFC.Player.Api.Filters;
using SFC.Player.Api.Services;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Api.Extensions;
using SFC.Player.Application;
using Microsoft.AspNetCore.Mvc;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Models.Base;

namespace SFC.Player.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddControllers(this IServiceCollection services)
    {
        services.AddControllers(configure =>
        {
            configure.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

            // Return 406 when Accept is not application/json
            configure.ReturnHttpNotAcceptable = true;

            // Accept and Content-Type headers filters
            configure.Filters.Add(new ProducesAttribute(CommonConstants.CONTENT_TYPE));
            configure.Filters.Add(new ConsumesAttribute(CommonConstants.CONTENT_TYPE));

            // Global responses filters
            configure.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            configure.Filters.Add(new ProducesResponseTypeAttribute(typeof(BaseResponse), StatusCodes.Status500InternalServerError));

            configure.Filters.Add(new ValidationFilterAttribute());
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        })
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
        .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(Resources)));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        // required for seed test players
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddScoped<IUserService, UserServiceDevelopment>();
        }
        else
        {
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
