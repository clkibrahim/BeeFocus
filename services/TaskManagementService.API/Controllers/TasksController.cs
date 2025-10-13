using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementService.Application.Features.Tasks.Commands.CreateTask;
using TaskManagementService.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagementService.Application.Features.Tasks.Commands.DeleteTask;
using TaskManagementService.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagementService.Application.Features.Tasks.Queries.GetTasks;

namespace TaskManagementService.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ISender _mediator;

        public TasksController(ISender mediator)
        {
            _mediator = mediator;
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestDto request)
        {
            var command = new CreateTaskCommand { Name = request.Name };
            var taskId = await _mediator.Send(command);
            return StatusCode(201, new { TaskId = taskId });
        }

        //READ
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _mediator.Send(new GetTasksQuery());
            return Ok(tasks);
        }

        //READ BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var query = new GetTaskByIdQuery { Id = id };
            var task = await _mediator.Send(query);
            return Ok(task);
        }

        //UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskRequestDto request)
        {
            var command = new UpdateTaskCommand
            {
                Id = id,
                Name = request.Name
            };

            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand { Id = id });
            return NoContent();
        }
    }
}    