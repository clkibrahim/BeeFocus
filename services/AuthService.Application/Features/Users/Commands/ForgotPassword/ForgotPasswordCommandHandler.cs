using AuthService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // 1. Kullanıcıyı e-postasına göre veritabanında bul.
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLowerInvariant(), cancellationToken);

        // 2. ÖNEMLİ: Eğer kullanıcı bulunamazsa, güvenlik nedeniyle "kullanıcı bulunamadı"
        // hatası DÖNMÜYORUZ. Sadece sessizce işlem tamamlanmış gibi davranıyoruz.
        // Bu, kötü niyetli kişilerin sistemdeki hangi e-postaların kayıtlı olduğunu tahmin etmesini engeller.
        if (user is not null)
        {
            // 3. Kullanıcı varsa, Domain katmanında yazdığımız metodu çağırarak token üretiyoruz.
            user.GeneratePasswordResetToken();

            // 4. Değişiklikleri (oluşturulan token ve son kullanma tarihi) veritabanına kaydediyoruz.
            await _context.SaveChangesAsync(cancellationToken);

            // 5. E-posta gönderme servisini kullanarak sıfırlama linkini gönderiyoruz.
            // Not: Gerçek bir uygulamada bu link, frontend uygulamamızın şifre sıfırlama sayfasına işaret eder.
            var resetLink = $"https://your-frontend-app.com/reset-password?token={user.PasswordResetToken}";
            var emailBody = $"Şifrenizi sıfırlamak için lütfen aşağıdaki linke tıklayın: <br><a href='{resetLink}'>{resetLink}</a>";
            
            await _emailService.SendEmailAsync(user.Email, "BeeFocus Şifre Sıfırlama Talebi", emailBody);
        }
        
        return Unit.Value;
    }
}