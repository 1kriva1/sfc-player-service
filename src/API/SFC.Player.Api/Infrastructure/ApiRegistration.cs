using System.Reflection;

using Microsoft.AspNetCore.Mvc;

namespace SFC.Player.Api.Infrastructure;

public static class ApiRegistration
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());
        services.Configure<MvcOptions>(options => options.AllowEmptyInputInBodyModelBinding = true);
        services.AddCors();
    }
}