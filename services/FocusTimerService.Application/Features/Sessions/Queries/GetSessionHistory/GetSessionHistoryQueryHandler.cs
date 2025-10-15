using MediatR;
using Microsoft.EntityFrameworkCore;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetSessionHistory;

public class GetSessionHistoryQueryHandler : IRequestHandler<GetSessionHistoryQuery, List<SessionHistoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetSessionHistoryQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<SessionHistoryDto>> Handle(GetSessionHistoryQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var history = await _context.FocusSessions
            .Where(s => s.UserId == userId && s.Status != SessionStatus.InProgress)
            .OrderByDescending(s => s.StartTime)
            .Select(s => new SessionHistoryDto
            {
                Id = s.Id,
                TaskId = s.TaskId,
                Type = s.Type,
                Status = s.Status,
                StartTime = s.StartTime,
                ActualDurationInSeconds = s.ActualDurationInSeconds
            })
            .ToListAsync(cancellationToken);

        return history;
    }
}