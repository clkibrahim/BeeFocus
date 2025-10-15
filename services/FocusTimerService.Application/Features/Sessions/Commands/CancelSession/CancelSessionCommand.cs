using MediatR;

namespace FocusTimerService.Application.Features.Sessions.Commands.CancelSession;

public class CancelSessionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}