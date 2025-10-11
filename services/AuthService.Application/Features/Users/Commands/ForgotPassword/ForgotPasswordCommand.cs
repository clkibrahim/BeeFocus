using MediatR;

namespace AuthService.Application.Features.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; }
}