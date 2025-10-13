using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask;
using TaskManagementService.Application.Interfaces;
namespace TaskManagementService.Application.Features.Todos.Queries.GetTodoById;
public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public GetTodoByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context; _currentUserService = currentUserService;
    }
    public async Task<TodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var todo = await _context.Todos
            .Where(t => t.Id == request.Id && t.UserId == userId)
            .Select(t => new TodoDto { Id = t.Id, Title = t.Title, IsCompleted = t.IsCompleted, CreatedAt = t.CreatedAt })
            .FirstOrDefaultAsync(cancellationToken);
        if (todo is null) { throw new Exception("Yapılacak bulunamadı veya bu görevi görüntüleme yetkiniz yok."); }
        return todo;
    }
}