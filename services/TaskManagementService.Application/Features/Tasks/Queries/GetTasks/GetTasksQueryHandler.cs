using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Application.Interfaces;

namespace TaskManagementService.Application.Features.Tasks.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, System.Collections.Generic.List<TaskManagementService.Application.Features.Tasks.Queries.GetTasks.TaskDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTasksQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<System.Collections.Generic.List<TaskManagementService.Application.Features.Tasks.Queries.GetTasks.TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var tasks = await _context.Tasks
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new TaskManagementService.Application.Features.Tasks.Queries.GetTasks.TaskDto
            {
                Id = p.Id,
                Name = p.Name,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);
        return tasks;
    }
}
