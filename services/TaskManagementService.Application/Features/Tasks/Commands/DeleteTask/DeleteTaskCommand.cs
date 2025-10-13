using MediatR;
using System;

namespace TaskManagementService.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}