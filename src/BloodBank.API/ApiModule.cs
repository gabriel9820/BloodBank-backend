using Microsoft.OpenApi.Models;

namespace BloodBank.API;

public static class ApiModule
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddSwagger();

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
        });

        return services;
    }
}
