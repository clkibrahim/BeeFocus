using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Queries.GetSessionHistory;

public class SessionHistoryDto
{
    public Guid Id { get; set; }
    public Guid? TaskId { get; set; }
    public SessionType Type { get; set; }
    public SessionStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public int? ActualDurationInSeconds { get; set; }
}