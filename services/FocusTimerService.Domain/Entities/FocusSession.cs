using FocusTimerService.Domain.Enums; 

namespace FocusTimerService.Domain.Entities;

public class FocusSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? TaskId { get; private set; } // Boş olabilir ("serbest" seanslar için)

    public SessionType Type { get; private set; }
    public SessionStatus Status { get; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; } // Seans bitene kadar boştur

    public int? PlannedDurationInMinutes { get; private set; } // Sadece Geri Sayım için
    public int? ActualDurationInSeconds { get; private set; }  // Seans bittiğinde hesaplanacak

    // Entity Framework için private constructor
    private FocusSession() {}

    // Yeni bir seans başlatmak için kullanacağımız ana constructor
    public FocusSession(Guid userId, SessionType type, Guid? taskId, int? plannedDuration)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Type = type;
        TaskId = taskId;
        PlannedDurationInMinutes = plannedDuration;

        Status = SessionStatus.InProgress; // Her yeni seans "Devam Ediyor" olarak başlar
        StartTime = DateTime.UtcNow;
    }

    // --- İŞ MANTIK METOTLARI ---

    // Bir seansı "Tamamlandı" olarak bitirir.
    public void Complete()
    {
        if (Status == SessionStatus.InProgress)
        {
            Status = SessionStatus.Completed;
            EndTime = DateTime.UtcNow;
            ActualDurationInSeconds = (int)(EndTime.Value - StartTime).TotalSeconds;
        }
    }

    // Bir seansı "İptal Edildi" olarak bitirir.
    public void Cancel()
    {
        if (Status == SessionStatus.InProgress)
        {
            Status = SessionStatus.Cancelled;
            EndTime = DateTime.UtcNow;
            // İptal edilen seanslar için de süreyi kaydedebiliriz, bu bir ürün kararıdır.
            ActualDurationInSeconds = (int)(EndTime.Value - StartTime).TotalSeconds;
        }
    }
}