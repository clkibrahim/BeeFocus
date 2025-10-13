using MediatR;
namespace TaskManagementService.Application.Features.Todos.Commands.DeleteTodo;
public class DeleteTodoCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}