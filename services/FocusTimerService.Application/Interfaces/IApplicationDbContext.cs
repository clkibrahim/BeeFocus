using Microsoft.EntityFrameworkCore;
using FocusTimerService.Domain.Entities;

namespace FocusTimerService.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<FocusSession> FocusSessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}