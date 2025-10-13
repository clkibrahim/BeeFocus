using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Application.Interfaces;

namespace TaskManagementService.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        // 1. Silinecek görevi ID'sine ve kullanıcı ID'sine göre bul.
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == userId, cancellationToken);

        // 2. Görev bulunamadıysa veya bu kullanıcıya ait değilse hata fırlat.
        if (task is null)
        {
            throw new Exception("Görev bulunamadı veya bu görevi silme yetkiniz yok.");
        }

        // 3. ÖNEMLİ: Bu ana göreve bağlı olan tüm alt görevleri (Todo) de bul.
        var todosToDelete = await _context.Todos.Where(t => t.TaskId == request.Id).ToListAsync(cancellationToken);
        if (todosToDelete.Any())
        {
            _context.Todos.RemoveRange(todosToDelete);
        }

        // 4. Ana görevi context'ten kaldır.
        _context.Tasks.Remove(task);

        // 5. Tüm değişiklikleri (hem Todo'ların hem de Task'in silinmesi) veritabanına kaydet.
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}