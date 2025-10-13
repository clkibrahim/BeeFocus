using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Domain.Entity;
using Task = TaskManagementService.Domain.Entity.Task;

namespace TaskManagementService.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Task> Tasks { get; }
    DbSet <Todo> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}