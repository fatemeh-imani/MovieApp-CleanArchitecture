using Microsoft.OpenApi.Models;

namespace MovieApp.API.Extentions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            //AddSwaggerGen()
            //فعال کردن Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MovieApp API",
                    Version = "v1"
                });

                //AddSecurityDefinition()
                //معرفی JWT به Swagger

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter: Bearer {token}"
                    });

                //AddSecurityRequirement()
                //اعمال JWT روی Endpointها و نمایش دکمه Authorize

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                    }
                );
            });

            return services;
        }
    }
}
