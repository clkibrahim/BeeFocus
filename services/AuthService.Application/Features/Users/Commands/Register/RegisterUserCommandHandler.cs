using AuthService.Domain.Entities;
using AuthService.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Users.Commands.Register;

// Bu sınıf, MediatR'a der ki: "Eğer bir RegisterUserCommand gelirse, onu ben işlerim
// ve geriye bir şey (Unit) dönmem."
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    // Bu handler'ın çalışabilmesi için veritabanına erişmesi gerekiyor.
    // Buraya doğrudan DbContext'i değil, onun soyut bir arayüzünü (interface) isteyeceğiz.

    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: Gelen e-posta adresiyle zaten bir kullanıcı var mı diye kontrol et.
        // Bu kontrolü bir sonraki adımlarda ekleyeceğiz.

        var user = new User(request.Email, request.Username);

        user.SetPassword(request.Password);

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        // MediatR için geriye Unit.Value dönüyoruz.
        return Unit.Value;
    }
}