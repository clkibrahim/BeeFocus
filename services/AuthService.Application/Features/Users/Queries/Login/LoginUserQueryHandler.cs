using AuthService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Users.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserQueryHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        // 1. Kullanıcıyı e-postasına göre veritabanında bul.
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email.ToLowerInvariant(), cancellationToken);

        // 2. Kullanıcı bulunamadıysa veya şifre yanlışsa hata fırlat.
        //    'user is null' kontrolünü önce yapmak önemlidir.
        if (user is null || !user.VerifyPassword(request.Password))
        {
            // Gerçek bir uygulamada daha spesifik custom exception'lar fırlatılır.
            throw new Exception("E-posta veya şifre hatalı.");
        }

        // 3. Şifre doğruysa, token üreticisini çağır.
        var token = _jwtTokenGenerator.GenerateToken(user);

        // 4. Oluşturulan token'ı geri dön.
        return token;
    }
}