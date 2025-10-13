using MediatR;
using TaskManagementService.Application.Interfaces;
namespace TaskManagementService.Application.Features.Todos.Commands.DeleteTodo;
public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public DeleteTodoCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context; _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);
        if (todo is null || todo.UserId != userId) { throw new Exception("Yapılacak bulunamadı veya bu işlemi silme yetkiniz yok."); }
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}