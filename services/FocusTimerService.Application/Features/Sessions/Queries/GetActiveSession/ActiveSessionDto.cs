using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetActiveSession;

public class ActiveSessionDto
{
    public Guid Id { get; set; }
    public Guid? TaskId { get; set; }
    public SessionType Type { get; set; }
    public DateTime StartTime { get; set; }
    public int? PlannedDurationInMinutes { get; set; }
}