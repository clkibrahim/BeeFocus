using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Application.Features.Tasks.Queries.GetTasks;
using TaskManagementService.Application.Interfaces;

namespace TaskManagementService.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTaskByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var task = await _context.Tasks
            .Where(t => t.Id == request.Id && t.UserId == userId)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                CreatedAt = t.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (task is null)
        {
            throw new Exception("Görev bulunamadı veya bu görevi görüntüleme yetkiniz yok.");
        }

        return task;
    }
}