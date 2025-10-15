using MediatR;
using Microsoft.EntityFrameworkCore;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetActiveSession;

public class GetActiveSessionQueryHandler : IRequestHandler<GetActiveSessionQuery, ActiveSessionDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetActiveSessionQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ActiveSessionDto?> Handle(GetActiveSessionQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var activeSession = await _context.FocusSessions
            .Where(s => s.UserId == userId && s.Status == SessionStatus.InProgress)
            .Select(s => new ActiveSessionDto
            {
                Id = s.Id,
                TaskId = s.TaskId,
                Type = s.Type,
                StartTime = s.StartTime,
                PlannedDurationInMinutes = s.PlannedDurationInMinutes
            })
            .FirstOrDefaultAsync(cancellationToken);

        return activeSession;
    }
}