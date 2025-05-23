using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Infrastructure.Persistence.Interceptors;

namespace SFC.Player.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static void AddPersistenceServices(this WebApplicationBuilder builder)
    {
        // contexts
        builder.Services.AddDbContext<MetadataDbContext>(builder.Configuration, builder.Environment);
        builder.Services.AddDbContext<PlayerDbContext>(builder.Configuration, builder.Environment);
        builder.Services.AddDbContext<DataDbContext>(builder.Configuration, builder.Environment);
        builder.Services.AddDbContext<IdentityDbContext>(builder.Configuration, builder.Environment);        

        // interceptors
        builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        builder.Services.AddScoped<DispatchDomainEventsSaveChangesInterceptor>();
        builder.Services.AddScoped<DataEntitySaveChangesInterceptor>();
        builder.Services.AddScoped<UserEntitySaveChangesInterceptor>();

        // contexts by interfaces
        builder.Services.AddScoped<IMetadataDbContext, MetadataDbContext>();
        builder.Services.AddScoped<IPlayerDbContext, PlayerDbContext>();
        builder.Services.AddScoped<IDataDbContext, DataDbContext>();
        builder.Services.AddScoped<IIdentityDbContext, IdentityDbContext>();

        // repositories
        builder.Services.AddRepositories(builder.Configuration);
    }
}
