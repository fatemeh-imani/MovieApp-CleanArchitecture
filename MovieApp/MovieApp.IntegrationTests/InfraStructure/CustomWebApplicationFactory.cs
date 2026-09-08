using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Persistence;

namespace MovieApp.IntegrationTests.Infrastructure;

public class CustomWebApplicationFactory
    : WebApplicationFactory<global::Program>
{
    private readonly SqliteConnection _connection;

    public CustomWebApplicationFactory()
    {
        _connection = new SqliteConnection(
            "Data Source=:memory:");

        _connection.Open();
    }

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove SQL Server configuration
            services.RemoveAll<
                IDbContextOptionsConfiguration<ApplicationDbContext>>();

            // Add SQLite
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlite(_connection);
                });
        });
    }

    protected override IHost CreateHost(
        IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        // Create SQLite database
        using (var scope = host.Services.CreateScope())
        {
            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
        }

        // Seed after database has been created
        host.Services
            .SeedInfrastructureAsync()
            .GetAwaiter()
            .GetResult();

        return host;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection.Dispose();
        }

        base.Dispose(disposing);
    }
}