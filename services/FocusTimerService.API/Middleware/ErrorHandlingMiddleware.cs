using System.Net;
using System.Text.Json;
using FluentValidation;

namespace FocusTimerService.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Beklenmedik bir hata oluştu: {Message}", exception.Message);

        HttpStatusCode statusCode;
        object errorResponse;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest; // 400
                errorResponse = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                break;

            // Buraya ileride NotFoundException gibi başka özel hatalar da eklenebilir.

            default:
                statusCode = HttpStatusCode.InternalServerError; // 500
                errorResponse = new { error = "Sunucuda beklenmedik bir hata oluştu." };
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(result);
    }
}