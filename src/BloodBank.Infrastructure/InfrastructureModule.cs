using BloodBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBloodBankDbContext(configuration);

        return services;
    }

    private static IServiceCollection AddBloodBankDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BloodBankDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BloodBankDb")));

        return services;
    }
}
