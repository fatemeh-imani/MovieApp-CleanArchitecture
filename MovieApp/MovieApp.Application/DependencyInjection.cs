
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.Behaviors;

namespace MovieApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
           
            services.AddValidatorsFromAssembly(
                typeof(DependencyInjection).Assembly,
                 includeInternalTypes: true);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(DependencyInjection).Assembly);

                cfg.AddOpenBehavior(
                    typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
