using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Infrastructure.Authentication;
using MovieApp.Infrastructure.Authentication.Identity;
using MovieApp.Infrastructure.Persistence;
using MovieApp.Infrastructure.BackgroundJobs;
using Quartz;
using System.Text;


namespace MovieApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("MovieCleanConnection"));

                options.AddInterceptors(
                    sp.GetRequiredService<
                        ConvertDomainEventsToOutboxMessagesInterceptor>());
            });

            services.AddQuartz(configurator =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configurator.AddJob<ProcessOutboxMessagesJob>(options =>
                {
                    options.WithIdentity(jobKey);
                });

                configurator.AddTrigger(options =>
                {
                    options.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                        {
                            schedule
                                .WithIntervalInSeconds(10)
                                .RepeatForever();
                        });
                });
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.SectionName));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                   var jwtOptions = configuration
                      .GetSection(JwtOptions.SectionName)
                      .Get<JwtOptions>()!;

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                       ValidateIssuer = true,
                       ValidIssuer = jwtOptions.Issuer,
                       ValidateAudience = true,
                       ValidAudience = jwtOptions.Audience,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                       ValidateLifetime = true
                  };
    });
           
           services.AddIdentityCore<ApplicationUser>()
              .AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization();


            services.AddScoped<IApplicationDbContext>(
                sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IIdentityService, IdentityService>();



            return services;
        }

        public static async Task SeedInfrastructureAsync(
                     this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            await IdentitySeeder.SeedAsync(
                scope.ServiceProvider);
        }
    }
}
