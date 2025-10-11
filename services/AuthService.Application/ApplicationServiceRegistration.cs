using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthService.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Bu kod, Application katmanındaki tüm Command, Query ve Handler'ları otomatik olarak bulur ve kaydeder.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Bu kod, Application katmanındaki tüm Validator sınıflarını otomatik olarak bulur ve kaydeder.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}