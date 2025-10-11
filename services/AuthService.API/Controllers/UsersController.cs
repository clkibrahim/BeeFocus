using AuthService.Application.Features.Users.Commands.Register;
using AuthService.Application.Features.Users.Queries.Login; // <-- BU SATIRI EKLE
using AuthService.Application.Features.Users.Commands.ForgotPassword;
using AuthService.Application.Features.Users.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(201); // 201 Created
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            var token = await _mediator.Send(query);
            return Ok(new { Token = token });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            // Kullanıcıya her zaman başarılı cevabı dönüyoruz (güvenlik nedeniyle).
            return Ok(new
                { Message = "Eğer e-posta adresiniz sistemimizde kayıtlıysa, şifre sıfırlama linki gönderilmiştir." });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Şifreniz başarıyla güncellenmiştir." });
        }
    }
}        