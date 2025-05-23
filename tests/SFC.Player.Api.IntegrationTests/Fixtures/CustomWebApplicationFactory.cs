//using System.Data.Common;

//using MassTransit;

//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Data.Sqlite;
//using Microsoft.EntityFrameworkCore;

//using Microsoft.Extensions.DependencyInjection;

//using SFC.Player.Infrastructure.Consumers.Data;
//using SFC.Player.Infrastructure.Persistence;
//using SFC.Player.Infrastructure.Persistence.Contexts;

//namespace SFC.Player.Api.IntegrationTests.Fixtures;

//public class CustomWebApplicationFactory<TStartup>
//       : WebApplicationFactory<TStartup> where TStartup : class
//{
//    private const string TEST_ENVIROMENT = "Testing";

//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        builder.ConfigureServices(services =>
//        {
//            // remove db contexts
//            RemoveServiceDescriptor<DbContextOptions<PlayerDbContext>>(services);

//            // remove db connection
//            RemoveServiceDescriptor<DbConnection>(services);

//            // Create open SqliteConnection so EF won't automatically close it.
//            services.AddSingleton<DbConnection>(container =>
//            {
//                SqliteConnection connection = new("DataSource=:memory:");
//                connection.Open();

//                return connection;
//            });

//            // switch db context connection to sqllite db
//            services.AddDbContext<PlayerDbContext>(SwitchToSqliteConnection);
//            services.AddDbContext<DataDbContext>(SwitchToSqliteConnection);
//            services.AddDbContext<IdentityDbContext>(SwitchToSqliteConnection);

//            services.AddMassTransitTestHarness(configure => configure.AddConsumer<DataInitializedConsumer>());
//        });
//    }

//    public void InitializeDbForTests()
//    {
//        using IServiceScope scope = Services.CreateScope();

//        IServiceProvider scopedServices = scope.ServiceProvider;

//        PlayerDbContext context = scopedServices.GetRequiredService<PlayerDbContext>();
//        IdentityDbContext identityContext = scopedServices.GetRequiredService<IdentityDbContext>();
//        DataDbContext dataContext = scopedServices.GetRequiredService<DataDbContext>();

//        context.RefreshData(identityContext, dataContext);
//    }

//    private static void RemoveServiceDescriptor<T>(IServiceCollection services)
//    {
//        ServiceDescriptor? serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
//        services.Remove(serviceDescriptor!);
//    }

//    private static void SwitchToSqliteConnection(IServiceProvider container, DbContextOptionsBuilder options)
//    {
//        DbConnection connection = container.GetRequiredService<DbConnection>();
//        options.UseSqlite(connection);
//    }
//}