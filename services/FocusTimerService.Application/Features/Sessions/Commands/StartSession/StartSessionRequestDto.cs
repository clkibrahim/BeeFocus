using FocusTimerService.Domain.Enums;

namespace FocusTimerService.Application.Features.Sessions.Commands.StartSession;

public class StartSessionRequestDto
{
    public SessionType Type { get; set; }
    public Guid? TaskId { get; set; }
    public int? PlannedDurationInMinutes { get; set; }
}