using MediatR;
using System;

namespace TaskManagementService.Application.Features.Todos.Commands.MarkAsCompleted;

public class MarkTodoAsCompletedCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}