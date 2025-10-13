using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Application.Interfaces;
using TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask;

namespace TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask;

public class GetTodosForTaskQueryHandler : IRequestHandler<GetTodosForTaskQuery, List<TodoDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTodosForTaskQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<TodoDto>> Handle(GetTodosForTaskQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        // --- GÜVENLİK KONTROLÜ ---
        // 1. Önce, kullanıcının bu projeyi görme yetkisi var mı diye kontrol et.
        var task = await _context.Tasks
            .FirstOrDefaultAsync(p => p.Id == request.TaskId && p.UserId == userId, cancellationToken);

        if (task is null)
        {
            // Eğer proje bulunamazsa veya kullanıcıya ait değilse, hata fırlat.
            throw new Exception("Proje bulunamadı veya bu projeyi görüntüleme yetkiniz yok.");
        }

        // 2. Yetki kontrolü başarılıysa, SADECE bu projeye ait olan görevleri çek.
        var todos = await _context.Todos
            .Where(t => t.TaskId == request.TaskId)
            .OrderBy(t => t.CreatedAt) // En eski olan en üstte gelsin (klasik todo listesi mantığı).
            .Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return todos;
    }
}