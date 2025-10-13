using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementService.Application.Features.Todos.Commands.CreateTodo;
using TaskManagementService.Application.Features.Todos.Commands.DeleteTodo;
using TaskManagementService.Application.Features.Todos.Commands.MarkAsCompleted;
using TaskManagementService.Application.Features.Todos.Commands.MarkAsIncomplete;
using TaskManagementService.Application.Features.Todos.Commands.UpdateTodoTitle;
using TaskManagementService.Application.Features.Todos.Queries.GetTodoById;
using TaskManagementService.Application.Features.Todos.Queries.GetTodosForTask;

namespace TaskManagementService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Tags("Todos")] // Swagger'da bu controller'a ait tüm endpoint'leri "Todos" grubu altında topla.
    public class TodosController : ControllerBase
    {
        private readonly ISender _mediator;

        public TodosController(ISender mediator)
        {
            _mediator = mediator;
        }
        
        // --- BİR ANA GÖREVE BAĞLI KOLEKSİYON İŞLEMLERİ ---
        // Bu endpoint'ler, bir "Task" bağlamında çalıştığı için URL'leri "/api/tasks/..." ile başlar.

        // POST /api/tasks/{taskId}/todos
        [HttpPost("/api/tasks/{taskId}/todos")] 
        public async Task<IActionResult> CreateTodoForTask(Guid taskId, [FromBody] CreateTodoRequestDto request)
        {
            var command = new CreateTodoCommand { TaskId = taskId, Title = request.Title };
            var todoId = await _mediator.Send(command);
            return StatusCode(201, new { TodoId = todoId });
        }

        // GET /api/tasks/{taskId}/todos
        [HttpGet("/api/tasks/{taskId}/todos")]
        public async Task<IActionResult> GetTodosForTask(Guid taskId)
        {
            var query = new GetTodosForTaskQuery { TaskId = taskId };
            var todos = await _mediator.Send(query);
            return Ok(todos);
        }

        // --- TEKİL BİR TODO ÜZERİNDEKİ İŞLEMLER ---
        // Bu endpoint'ler, kimliği zaten belli olan tek bir "Todo" üzerinde çalıştığı için
        // URL'leri doğrudan "/api/todos/..." ile başlar.

        // GET /api/todos/{id}
        [HttpGet("/api/todos/{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var query = new GetTodoByIdQuery { Id = id };
            var todo = await _mediator.Send(query);
            return Ok(todo);
        }

        // PUT /api/todos/{id}
        [HttpPut("/api/todos/{id}")]
        public async Task<IActionResult> UpdateTodoTitle(Guid id, [FromBody] UpdateTodoTitleRequestDto request)
        {
            var command = new UpdateTodoTitleCommand { Id = id, Title = request.Title };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT /api/todos/{id}/complete
        [HttpPut("/api/todos/{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            var command = new MarkTodoAsCompletedCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT /api/todos/{id}/incomplete
        [HttpPut("/api/todos/{id}/incomplete")]
        public async Task<IActionResult> MarkAsIncomplete(Guid id)
        {
            var command = new MarkTodoAsIncompleteCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE /api/todos/{id}
        [HttpDelete("/api/todos/{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var command = new DeleteTodoCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}