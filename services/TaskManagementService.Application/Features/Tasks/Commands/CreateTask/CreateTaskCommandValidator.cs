using FluentValidation;

namespace TaskManagementService.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Görev adı boş olamaz.")
            .MaximumLength(100).WithMessage("Görev adı 100 karakterden uzun olamaz.");
    }
}