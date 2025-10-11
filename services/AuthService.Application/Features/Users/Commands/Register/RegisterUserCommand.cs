using MediatR;

namespace AuthService.Application.Features.Users.Commands.Register;

// MediatR kütüphanesine, bu sınıfın bir "istek" olduğunu ve işlendikten sonra
// geriye bir şey dönmeyeceğini (Unit, void'in karşılığıdır) söylüyoruz.
public class RegisterUserCommand : IRequest<Unit>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}