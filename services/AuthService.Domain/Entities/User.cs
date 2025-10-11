using System.Security.Cryptography;
using System.Text;

namespace AuthService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? PasswordResetToken { get; private set; }
    public DateTime? ResetTokenExpires { get; private set; }
    

    // Entity Framework Core gibi ORM'lerin bu sınıfı veritabanından okurken
    // "new"leyebilmesi için private bir constructor ekliyoruz.
    // Kodumuzun geri kalanının bu constructor'ı kullanmasını istemediğimiz için private yapıyoruz.
    private User() {}


    public User(string email, string username)
    {
        Id = Guid.NewGuid();
        Email = email.ToLowerInvariant(); 
        Username = username;
        CreatedAt = DateTime.UtcNow; 
        
    }

    public void SetPassword(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordSalt = hmac.Key;
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifyPassword(string password)
    {
        using var hmac = new HMACSHA512(PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(PasswordHash);
    }

    public void GeneratePasswordResetToken()
    {
        PasswordResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        ResetTokenExpires = DateTime.UtcNow.AddMinutes(15); 
    }
    public void ClearPasswordResetToken()
    {
        PasswordResetToken = null;
        ResetTokenExpires = null;
    }
}