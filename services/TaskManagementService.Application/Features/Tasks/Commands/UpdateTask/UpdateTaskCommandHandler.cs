using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementService.Application.Interfaces;

namespace TaskManagementService.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var task = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

        if (task is null || task.UserId != userId)
        {
            throw new Exception("Task bulunamadı veya bu task'i güncelleme yetkiniz yok.");
        }

        task.UpdateName(request.Name);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}