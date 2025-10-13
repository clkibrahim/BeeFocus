namespace TaskManagementService.Domain.Entity;

public class Task
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid UserId { get; private set; } // <-- EN ÖNEMLİ ALAN!
    public DateTime CreatedAt { get; private set; }

    private Task() {}

    // Yeni bir proje oluştururken kullanacağımız ana constructor.
    public Task(string name, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        UserId = userId; // Bu projenin kime ait olduğunu belirtiyoruz.
        CreatedAt = DateTime.UtcNow;
    }

    // Projenin adını değiştirmek için bir metot (ileride kullanabiliriz).
    public void UpdateName(string newName)
    {
        Name = newName;
    }
}