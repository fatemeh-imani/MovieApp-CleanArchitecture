
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MovieApp.Appliccation
{
    public static class DependencyInjecction
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cgf =>
            {
                cgf.RegisterServicesFromAssemblies(
                typeof(DependencyInjecction).Assembly);
            });

            services.AddValidatorsFromAssembly(
                typeof(DependencyInjecction).Assembly);

            return services;
        }
    }
}
