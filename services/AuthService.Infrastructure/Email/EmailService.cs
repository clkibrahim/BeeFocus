using AuthService.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // E-posta mesajını oluşturuyoruz.
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(
            _configuration["MailSettings:FromName"],
            _configuration["MailSettings:FromAddress"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        // SmtpClient (MailKit'in e-posta göndericisi) ile e-postayı gönderiyoruz.
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _configuration["MailSettings:Host"],
            int.Parse(_configuration["MailSettings:Port"]!));

        // MailHog'un kullanıcı adı/şifresi olmadığı için Authenticate adımını atlıyoruz.

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}