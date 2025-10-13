namespace TaskManagementService.Domain.Entity;

public class Todo 
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    
    // --- İlişki Anahtarları ---
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Todo() {}

    public Todo(string title, Guid taskId, Guid userId)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = false;
        TaskId = taskId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string newTitle)
    {
        Title = newTitle;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void MarkAsIncomplete()
    {
        IsCompleted = false;
    }
}