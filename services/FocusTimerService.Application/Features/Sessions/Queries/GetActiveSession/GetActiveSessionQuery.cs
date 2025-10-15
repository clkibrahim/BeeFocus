using MediatR;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetActiveSession;

public class GetActiveSessionQuery : IRequest<ActiveSessionDto?> // Cevap null olabilir
{
}