using MediatR;

namespace FocusTimerService.Application.Features.Sessions.Commands.CompleteSession;

public class CompleteSessionCommand : IRequest<Unit>
{
    public Guid Id { get; set; } // Hangi seansın tamamlanacağı
}