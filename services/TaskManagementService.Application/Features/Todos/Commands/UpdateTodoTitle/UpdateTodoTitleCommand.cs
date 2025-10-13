using MediatR;
namespace TaskManagementService.Application.Features.Todos.Commands.UpdateTodoTitle;
public class UpdateTodoTitleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}