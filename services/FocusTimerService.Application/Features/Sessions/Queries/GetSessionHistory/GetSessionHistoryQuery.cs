using MediatR;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetSessionHistory;

public class GetSessionHistoryQuery : IRequest<List<SessionHistoryDto>>
{
}