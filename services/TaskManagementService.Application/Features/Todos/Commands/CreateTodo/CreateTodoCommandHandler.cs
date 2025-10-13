using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Application.Interfaces;
using TaskManagementService.Domain.Entity;

namespace TaskManagementService.Application.Features.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTodoCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        // --- EN ÖNEMLİ GÜVENLİK KONTROLÜ ---
        // 1. Görevin ekleneceği task'in, bu kullanıcıya ait olup olmadığını kontrol et.
        var taskExists = await _context.Tasks
            .AnyAsync(p => p.Id == request.TaskId && p.UserId == userId, cancellationToken);

        if (!taskExists)
        {
            // Eğer task bulunamazsa veya kullanıcıya ait değilse, asla todo oluşturma.
            throw new Exception("Task bulunamadı veya bu task'e todo ekleme yetkiniz yok.");
        }
        // --- GÜVENLİK KONTROLÜ SONU ---

        // 2. Güvenlik kontrolü başarılıysa, yeni Todo entity'sini oluştur.
        var todo = new Todo(request.Title, request.TaskId, userId);

        // 3. Entity'yi veritabanına eklenmek üzere hazırla.
        await _context.Todos.AddAsync(todo, cancellationToken);

        // 4. Değişiklikleri veritabanına kaydet.
        await _context.SaveChangesAsync(cancellationToken);

        // 5. Oluşturulan yeni görevin ID'sini geri dön.
        return todo.Id;
    }
}