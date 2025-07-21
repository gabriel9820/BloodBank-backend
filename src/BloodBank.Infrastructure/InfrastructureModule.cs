using System.Text;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Auth;
using BloodBank.Infrastructure.Models;
using BloodBank.Infrastructure.Persistence;
using BloodBank.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;

namespace BloodBank.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .LoadConfiguration(configuration)
            .AddBloodBankDbContext(configuration)
            .AddRepositories()
            .AddAuthentication(configuration)
            .AddEmailService(configuration);

        return services;
    }

    public static IServiceCollection LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StockConfig>(configuration.GetSection("StockConfig"));
        services.Configure<SendGridConfig>(configuration.GetSection("SendGridConfig"));

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
        services.AddScoped<IDonorRepository, DonorRepository>();
        services.AddScoped<IHospitalRepository, HospitalRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IBloodTransferRepository, BloodTransferRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<IEmailService, EmailService>();
        services.AddSendGrid(options =>
        {
            options.ApiKey = configuration["SendGrid:ApiKey"];
        });

        return services;
    }
}
