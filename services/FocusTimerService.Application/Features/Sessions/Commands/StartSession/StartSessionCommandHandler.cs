using MediatR;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.Domain.Entities;

namespace FocusTimerService.Application.Features.Sessions.Commands.StartSession;

public class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public StartSessionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(StartSessionCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        // TODO: İleride, gelen TaskId'nin gerçekten bu kullanıcıya ait olup olmadığını TaskManagementService'e sorarak doğrulayabiliriz. (Servisler arası iletişim)

        // 1. Domain katmanındaki constructor'ı kullanarak yeni bir seans nesnesi oluştur.
        var session = new FocusSession(
            userId: userId,
            type: request.Type,
            taskId: request.TaskId,
            plannedDuration: request.PlannedDurationInMinutes);

        // 2. Entity'yi veritabanına eklenmek üzere hazırla.
        await _context.FocusSessions.AddAsync(session, cancellationToken);

        // 3. Değişiklikleri veritabanına kaydet.
        await _context.SaveChangesAsync(cancellationToken);

        // 4. Oluşturulan yeni seansın ID'sini geri dön. Bu ID, seansı bitirmek için kullanılacak.
        return session.Id;
    }
}