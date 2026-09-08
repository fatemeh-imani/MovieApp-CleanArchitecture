using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MovieApp.Application;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Infrastructure.Authentication;
using MovieApp.Infrastructure.Authentication.Identity;
using MovieApp.Infrastructure.Persistence;


namespace MovieApp.IntegrationTests.InfraStructure
{
    public static class TestServiceProviderFactory
    {
        public static IServiceProvider Create()
        {
            var services = new ServiceCollection();

            // SQLite
            var connection =
                new SqliteConnection("DataSource=:memory:");

            connection.Open();

            services.AddSingleton(connection);

            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlite(connection);
                });

            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(DependencyInjection).Assembly);
            });

            // Identity
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Logging
            services.AddLogging();

            // JWT Options
            services.AddSingleton(
                Options.Create(new JwtOptions
                {
                    SecretKey =
                        "test-secret-key-12345678901234567890",
                    Issuer = "MovieApp-Test",
                    Audience = "MovieApp-Test",
                    ExpirationInMinutes = 60
                }));

            // JWT
            services.AddScoped<IJwtProvider, JwtProvider>();

            // Identity Service
            services.AddScoped<IIdentityService, IdentityService>();

            return services.BuildServiceProvider();
        }
    }
}
