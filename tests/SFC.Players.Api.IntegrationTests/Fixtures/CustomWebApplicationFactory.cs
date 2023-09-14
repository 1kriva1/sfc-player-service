﻿using System.Data.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Infrastructure.Persistence;

namespace SFC.Players.Api.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string TEST_ENVIROMENT = "Testing";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<PlayersDbContext>));

            services.Remove(dbContextDescriptor!);

            ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbConnection));

            services.Remove(dbConnectionDescriptor!);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                SqliteConnection connection = new("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<PlayersDbContext>((container, options) =>
            {
                DbConnection connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });            
        });

        builder.UseEnvironment(TEST_ENVIROMENT);
    }

    public void InitializeDbForTests()
    {
        using IServiceScope scope = Services.CreateScope();

        IServiceProvider scopedServices = scope.ServiceProvider;

        PlayersDbContext context = scopedServices.GetRequiredService<PlayersDbContext>();

        context.Database.EnsureCreated();        

        context.Players.ExecuteDelete();
    }
}
