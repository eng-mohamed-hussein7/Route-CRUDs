using Application.GlobalResponse;
using System.ComponentModel.DataAnnotations;

namespace Presentation.MiddlewareConfig;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            var correlationId = context.Items["X-Correlation-Id"]?.ToString();

            // log structured
            _logger.LogError(ex,
                "Unhandled exception at {Path}. Method: {Method}. CorrelationId: {CorrelationId}",
                context.Request.Path,
                context.Request.Method,
                correlationId);

            context.Response.ContentType = "application/json";

            Response response;
            switch (ex)
            {
                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response = Response.Failure("Unauthorized access.", ResponseStatus.Failed);
                    break;

                case ValidationException ve:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Response.Validation(ve.Message);
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = Response.NotFound("The requested resource was not found.");
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = Response.Failure(
                        "Something went wrong! Please contact support.",
                        ResponseStatus.Failed
                    );
                    break;
            }

            // دايمًا نرجع CorrelationId للـ Client عشان يتابع مع الـ Support
            var wrapped = new
            {
                response.Succeeded,
                response.Status,
                response.Message,
                response.Error,
                response.Data,
                CorrelationId = correlationId
            };

            await context.Response.WriteAsJsonAsync(wrapped);
        }
    }
}
