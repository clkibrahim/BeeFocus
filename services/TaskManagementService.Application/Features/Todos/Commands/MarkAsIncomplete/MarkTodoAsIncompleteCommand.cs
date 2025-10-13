using MediatR;
namespace TaskManagementService.Application.Features.Todos.Commands.MarkAsIncomplete;
public class MarkTodoAsIncompleteCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}