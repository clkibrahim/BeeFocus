using MediatR;
using TaskManagementService.Application.Interfaces;
namespace TaskManagementService.Application.Features.Todos.Commands.MarkAsCompleted;
public class MarkTodoAsCompletedCommandHandler : IRequestHandler<MarkTodoAsCompletedCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public MarkTodoAsCompletedCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context; _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(MarkTodoAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);
        if (todo is null || todo.UserId != userId) { throw new Exception("Yapılacak bulunamadı veya bu işlemi yapma yetkiniz yok."); }
        todo.MarkAsCompleted();
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}