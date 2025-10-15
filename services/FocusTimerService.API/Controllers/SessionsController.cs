using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FocusTimerService.Application.Features.Sessions.Commands.StartSession;
using FocusTimerService.Application.Features.Sessions.Commands.CompleteSession;
using FocusTimerService.Application.Features.Sessions.Commands.CancelSession;
using FocusTimerService.Application.Features.Sessions.Queries.GetActiveSession;
using FocusTimerService.Application.Features.Sessions.Queries.GetSessionHistory;

namespace FocusTimerService.API.Controllers
{
    [ApiController]
    [Route("api/sessions")] // Bu controller'ın ana adresi /api/sessions olacak
    [Authorize]
    [Tags("Sessions")] // Swagger'da "Sessions" grubu altında topla
    public class SessionsController : ControllerBase
    {
        private readonly ISender _mediator;

        public SessionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        // POST /api/sessions
        // Yeni bir odaklanma seansı başlatır
        [HttpPost]
        public async Task<IActionResult> StartSession([FromBody] StartSessionRequestDto request)
        {
            var command = new StartSessionCommand
            {
                Type = request.Type,
                TaskId = request.TaskId,
                PlannedDurationInMinutes = request.PlannedDurationInMinutes
            };
            
            var sessionId = await _mediator.Send(command);
            return StatusCode(201, new { SessionId = sessionId });
        }

        // PUT /api/sessions/{id}/complete
        // Devam eden bir seansı "Tamamlandı" olarak bitirir
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteSession(Guid id)
        {
            var command = new CompleteSessionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT /api/sessions/{id}/cancel
        // Devam eden bir seansı "İptal Edildi" olarak bitirir
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelSession(Guid id)
        {
            var command = new CancelSessionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // GET /api/sessions/active
        // Kullanıcının devam eden bir seansı olup olmadığını kontrol eder
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveSession()
        {
            var activeSession = await _mediator.Send(new GetActiveSessionQuery());
            if (activeSession is null)
            {
                return NoContent(); // Aktif seans yoksa 204 No Content dönmek daha doğru
            }
            return Ok(activeSession);
        }

        // GET /api/sessions
        // Kullanıcının geçmiş tüm seanslarını listeler
        [HttpGet]
        public async Task<IActionResult> GetSessionHistory()
        {
            var history = await _mediator.Send(new GetSessionHistoryQuery());
            return Ok(history);
        }
    }
}