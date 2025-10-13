using MediatR;
using TaskManagementService.Application.Interfaces;
namespace TaskManagementService.Application.Features.Todos.Commands.MarkAsIncomplete;
public class MarkTodoAsIncompleteCommandHandler : IRequestHandler<MarkTodoAsIncompleteCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public MarkTodoAsIncompleteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context; _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(MarkTodoAsIncompleteCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);
        if (todo is null || todo.UserId != userId) { throw new Exception("Yapılacak bulunamadı veya bu işlemi yapma yetkiniz yok."); }
        todo.MarkAsIncomplete();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}