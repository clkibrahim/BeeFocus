using MediatR;

namespace AuthService.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<Unit>
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}