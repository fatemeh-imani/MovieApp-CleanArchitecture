using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.API.Exceptions
{

    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
           
            if (exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                   .GroupBy(x => x.PropertyName)
                   .ToDictionary(
                     g => g.Key,
                     g => g.Select(x => x.ErrorMessage).ToArray());

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(
                    new ValidationProblemDetails(errors)
                    {
                        Title = "Validation Error",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "One or more validation errors occurred."
                    },
                    cancellationToken);

                return true;
            }

            logger.LogError(
                      exception,
                    "An unhandled exception occurred.");

            return false;
        }
    }
}