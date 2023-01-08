using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;

namespace CurrencyObserver.Middleware;

internal class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate continueTask)
    {
        try
        {
            await continueTask(context);
        }
        catch (Exception exception)
        {
            if (!string.IsNullOrEmpty(exception.Message))
            {
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogError(exception, exception.Message);
            }

            await HandleExceptionInternalAsync(context, exception);
        }
    }

    private static async Task HandleExceptionInternalAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetValidationFailures(exception) 
                     ?? ArraySegment<ValidationFailure>.Empty
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        var jObject = JObject.FromObject(response);
        
        await httpContext.Response.WriteAsync(jObject.ToString());
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException { Source: { } } applicationException => applicationException.Source,
            _ => "Server Error"
        };

    private static IEnumerable<ValidationFailure>? GetValidationFailures(Exception exception)
    {
        IEnumerable<ValidationFailure>? validationFailures = null;
        if (exception is ValidationException validationException)
        {
            validationFailures = validationException.Errors;
        }

        return validationFailures;
    }
}