using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.Domain.Entities;

namespace FocusTimerService.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<FocusSession> FocusSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Bu satır, veritabanı tablo isimlerini ve kolon tiplerini
        // daha detaylı ayarlamak istediğimizde kullanacağımız yapılandırma dosyalarını
        // otomatik olarak bulur. Şimdilik sadece iyi bir alışkanlık.
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}