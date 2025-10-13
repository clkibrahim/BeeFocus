namespace TaskManagementService.Application.Features.Tasks.Queries.GetTasks;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}