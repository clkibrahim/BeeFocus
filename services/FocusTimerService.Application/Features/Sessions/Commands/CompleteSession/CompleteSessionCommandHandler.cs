using MediatR;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Commands.CompleteSession;

public class CompleteSessionCommandHandler : IRequestHandler<CompleteSessionCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CompleteSessionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CompleteSessionCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var session = await _context.FocusSessions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (session is null || session.UserId != userId || session.Status != SessionStatus.InProgress)
        {
            throw new Exception("Seans bulunamadı, size ait değil veya zaten tamamlanmış.");
        }

        // Domain'deki akıllı metodu çağırıyoruz
        session.Complete();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}