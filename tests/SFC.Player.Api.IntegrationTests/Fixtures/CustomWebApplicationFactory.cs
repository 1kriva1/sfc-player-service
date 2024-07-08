using System.Data.Common;

using MassTransit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Infrastructure.Consumers;
using SFC.Player.Infrastructure.Persistence;

namespace SFC.Player.Api.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string TEST_ENVIROMENT = "Testing";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // remove db contexts
            RemoveServiceDescriptor<DbContextOptions<PlayerDbContext>>(services);

            // remove db connection
            RemoveServiceDescriptor<DbConnection>(services);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                SqliteConnection connection = new("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            // switch db context connection to sqllite db
            services.AddDbContext<PlayerDbContext>(SwitchToSqliteConnection);

            services.AddMassTransitTestHarness(configure => configure.AddConsumer<DataInitializationMessageConsumer>());
        });
    }

    public void InitializeDbForTests()
    {
        using IServiceScope scope = Services.CreateScope();

        IServiceProvider scopedServices = scope.ServiceProvider;

        PlayerDbContext context = scopedServices.GetRequiredService<PlayerDbContext>();

        context.RefreshData();
    }

    private static void RemoveServiceDescriptor<T>(IServiceCollection services)
    {
        ServiceDescriptor? serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        services.Remove(serviceDescriptor!);
    }

    private static void SwitchToSqliteConnection(IServiceProvider container, DbContextOptionsBuilder options)
    {
        DbConnection connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
    }
}