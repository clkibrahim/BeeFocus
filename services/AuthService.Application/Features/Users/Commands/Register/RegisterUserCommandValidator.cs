using FluentValidation;

namespace AuthService.Application.Features.Users.Commands.Register;

// Bu sınıf, FluentValidation'a der ki: "Ben bir RegisterUserCommand nesnesini doğrulamak için gereken kuralları içeriyorum."
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
public RegisterUserCommandValidator()
{
    RuleFor(x => x.Username)
        .NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz.")
        .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
        .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
        .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

    RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
        .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
        .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
        .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
        .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
        .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter (örn: !?*.) içermelidir.");
}
}