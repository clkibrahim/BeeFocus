using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthService.Persistence;

// Bu sınıf, hem EF Core'un DbContext'inden miras alır, hem de bizim sözleşmemizi (IApplicationDbContext) uygular.
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Bu satır, bu proje (Persistence) içindeki tüm IEntityTypeConfiguration sınıflarını
        // otomatik olarak bulup uygular. Bu, DbContext'i temiz tutar.
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}