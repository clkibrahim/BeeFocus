using MediatR;

namespace TaskManagementService.Application.Features.Tasks.Queries.GetTasks;

public class GetTasksQuery : IRequest<System.Collections.Generic.List<TaskManagementService.Application.Features.Tasks.Queries.GetTasks.TaskDto>>
{
    // Parametre yok; mevcut kullanıcının taskları dönecek.
}
