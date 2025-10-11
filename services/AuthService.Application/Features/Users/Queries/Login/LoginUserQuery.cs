using MediatR;

namespace AuthService.Application.Features.Users.Queries.Login;

// Bu sefer MediatR'a, bu isteğin işlendikten sonra geriye bir 'string' döneceğini söylüyoruz. Bu string, kullanıcının JWT'si olacak.
public class LoginUserQuery : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}