using MediatR;
using System;

namespace TaskManagementService.Application.Features.Todos.Commands.CreateTodo;

public class CreateTodoCommand : IRequest<Guid>
{
    public Guid TaskId { get; set; } // Hangi task'a eklenecek?
    public string Title { get; set; } // Görevin başlığı ne?
}