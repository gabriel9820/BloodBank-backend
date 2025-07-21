using BloodBank.Application.Commands.Register;
using BloodBank.Application.Events;
using BloodBank.Core.DomainEvents;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace BloodBank.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR()
            .AddFluentValidation()
            .AddDomainEventDispatcher();

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterCommand>());

        return services;
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssemblyContaining<RegisterValidator>()
            .AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }

}
