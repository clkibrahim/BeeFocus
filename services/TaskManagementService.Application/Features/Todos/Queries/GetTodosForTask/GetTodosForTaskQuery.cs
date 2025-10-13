using MediatR;
using System;
using System.Collections.Generic;

namespace TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask;

public class GetTodosForTaskQuery : IRequest<List<TodoDto>>
{
    public Guid TaskId { get; set; }
}