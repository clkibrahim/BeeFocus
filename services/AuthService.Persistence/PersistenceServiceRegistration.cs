using AuthService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Veritabanı context'imizi .NET'in servis koleksiyonuna ekliyoruz.
        services.AddDbContext<ApplicationDbContext>(options =>
            // Veritabanı olarak PostgreSQL kullanacağımızı ve bağlantı bilgisini
            // appsettings.json dosyasından alacağımızı belirtiyoruz.
            options.UseNpgsql(configuration.GetConnectionString("AuthServiceConnection")));

        // Çok Önemli: Birisi IApplicationDbContext istediğinde, ona ApplicationDbContext ver.
        // Bu satır, Application ve Persistence katmanlarını birbirine bağlar.
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}