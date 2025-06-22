using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Auth;
using BloodBank.Infrastructure.Persistence;
using BloodBank.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBloodBankDbContext(configuration)
            .AddRepositories()
            .AddAuthentication(configuration);

        return services;
    }

    private static IServiceCollection AddBloodBankDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BloodBankDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BloodBankDb")));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
