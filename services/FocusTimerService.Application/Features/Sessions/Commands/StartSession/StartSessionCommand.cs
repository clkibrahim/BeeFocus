using MediatR;
using FocusTimerService.Domain.Enums; // SessionType enum'ı için

namespace FocusTimerService.Application.Features.Sessions.Commands.StartSession;

// Yeni oluşturulan seansın ID'sini geri döneceğiz.
public class StartSessionCommand : IRequest<Guid>
{
    public SessionType Type { get; set; }
    public Guid? TaskId { get; set; } // Hangi ana göreve bağlı (opsiyonel)
    public int? PlannedDurationInMinutes { get; set; } // Sadece Geri Sayım modu için
}