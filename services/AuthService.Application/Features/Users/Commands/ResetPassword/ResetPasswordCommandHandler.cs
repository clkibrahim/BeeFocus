using AuthService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public ResetPasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // 1. Gelen token ile eşleşen bir kullanıcı var mı diye bul.
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token, cancellationToken);

        // 2. Eğer kullanıcı yoksa veya token'ın süresi dolmuşsa hata fırlat.
        if (user is null || user.ResetTokenExpires < DateTime.UtcNow)
        {
            throw new Exception("Geçersiz veya süresi dolmuş şifre sıfırlama token'ı.");
        }

        // 3. Her şey geçerliyse, Domain katmanındaki metotları çağır.
        user.SetPassword(request.NewPassword); // Yeni şifreyi hash'le ve set et.
        user.ClearPasswordResetToken();       // Token'ı temizle ki tekrar kullanılmasın.

        // 4. Değişiklikleri veritabanına kaydet.
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}