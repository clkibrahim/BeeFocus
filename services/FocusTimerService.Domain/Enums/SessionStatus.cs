namespace FocusTimerService.Domain.Enums;

public enum SessionStatus
{
    InProgress = 1, // Devam Ediyor
    Completed = 2,  // Tamamlandı
    Cancelled = 3   // İptal Edildi
}