using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace SFC.Player.Api.Infrastructure;

public static class ApiRegistration
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());        
        services.Configure<MvcOptions>(options => options.AllowEmptyInputInBodyModelBinding = true);
        services.AddCors();        
    }
}
