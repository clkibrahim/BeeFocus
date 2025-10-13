using MediatR;
using TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask; // TodoDto'yu kullanmak için
namespace TaskManagementService.Application.Features.Todos.Queries.GetTodoById;
public class GetTodoByIdQuery : IRequest<TodoDto>
{
    public Guid Id { get; set; }
}