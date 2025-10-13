using MediatR;
using System;

namespace TaskManagementService.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}