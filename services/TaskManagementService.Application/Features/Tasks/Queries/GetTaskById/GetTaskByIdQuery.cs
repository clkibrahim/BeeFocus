using MediatR;
using TaskManagementService.Application.Features.Tasks.Queries.GetTasks; // TaskDto için

namespace TaskManagementService.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdQuery : IRequest<TaskDto>
{
    public Guid Id { get; set; }
}