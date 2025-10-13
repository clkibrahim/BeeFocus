using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagementService.Application.Interfaces;
using TaskManagementService.Domain.Entity;
using Task = TaskManagementService.Domain.Entity.Task;

namespace TaskManagementService.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Task> Tasks { get; set; } 
    public DbSet<Todo> Todos { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}