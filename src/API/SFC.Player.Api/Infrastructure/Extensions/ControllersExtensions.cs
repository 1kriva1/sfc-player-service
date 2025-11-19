using Microsoft.AspNetCore.Mvc;

using SFC.Player.Api.Infrastructure.Extensions;
using SFC.Player.Api.Infrastructure.Filters;
using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Application;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Infrastructure.Constants;

namespace SFC.Player.Api.Infrastructure.Extensions;

public static class ControllersExtensions
{
    public static void AddControllers(this IServiceCollection services)
    {
        services.AddControllers(configure =>
        {
            configure.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

            // Return 406 when Accept is not application/json
            configure.ReturnHttpNotAcceptable = true;

            // Accept and Content-Type headers filters
            configure.Filters.Add(new ProducesAttribute(CommonConstants.JsonContentType, CommonConstants.GrpcContentType));
            configure.Filters.Add(new ConsumesAttribute(CommonConstants.JsonContentType, CommonConstants.GrpcContentType));

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
}