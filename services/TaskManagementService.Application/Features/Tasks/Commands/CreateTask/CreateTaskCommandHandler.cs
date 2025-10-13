using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Application.Interfaces;
using TaskManagementService.Domain.Entity;
using Task = TaskManagementService.Domain.Entity.Task;

namespace TaskManagementService.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // 1. Güvenli bir şekilde, isteği yapan kullanıcının kimliğini al.
        var userId = _currentUserService.UserId;

        // 2. Domain entity'sini oluştur. Projenin sahibini constructor'da belirtiyoruz.
        var task = new Task(request.Name, userId);

        // 3. Entity'yi veritabanına eklenmek üzere hazırla.
        await _context.Tasks.AddAsync(task, cancellationToken);

        // 4. Değişiklikleri veritabanına kaydet.
        await _context.SaveChangesAsync(cancellationToken);

        // 5. Oluşturulan yeni projenin ID'sini geri dön.
        return task.Id;
    }
}