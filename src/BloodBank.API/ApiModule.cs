using BloodBank.API.Middlewares;
using BloodBank.Application.DTOs;
using Microsoft.OpenApi.Models;

namespace BloodBank.API;

public static class ApiModule
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .LoadConfiguration(configuration)
            .AddSwagger()
            .AddExceptionHandler();

        return services;
    }

    public static IServiceCollection LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StockConfig>(configuration.GetSection("StockConfig"));

        return services;
    }

    private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BloodBank API",
                Version = "v1",
                Description = "API para gerenciamento de bancos de sangue",
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT no formato: Bearer {token}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
        });

        return services;
    }
}
