using FluentValidation;

namespace TaskManagementService.Application.Features.Todos.Commands.CreateTodo;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Yapılacak başlığı boş olamaz.")
            .MaximumLength(200).WithMessage("Yapılacak başlığı 200 karakterden uzun olamaz.");
    }
}