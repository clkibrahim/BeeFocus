using AuthService.Application.Interfaces;
using AuthService.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Infrastructure.Email;

namespace AuthService.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Singleton kullanıyoruz çünkü token üreticisi her istekte yeniden yaratılması gerekmeyen, durumu olmayan bir servis.
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}