using System.Net;
using BookBase.Api.Models.Responses;
using BookBase.Domain.Exceptions;

namespace BookBase.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                await HandleValidationExceptionAsync(context, validationException);
                break;
            case NotFoundException notFoundException:
                await HandleNotFoundExceptionAsync(context, notFoundException);
                break;
            default:
                await HandleDefaultExceptionAsync(context, exception);
                break;
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException validationException)
    {
        const int statusCode = (int)HttpStatusCode.BadRequest;
        context.Response.StatusCode = statusCode;
        var validationProblemDetailsResponse = new ValidationProblemDetailsResponse
        {
            Title = "Validation error",
            Errors = validationException.Errors,
            StatusCode = statusCode,
        };
        await context.Response.WriteAsJsonAsync(validationProblemDetailsResponse);
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException notFoundException)
    {
        const int statusCode = (int)HttpStatusCode.NotFound;
        context.Response.StatusCode = statusCode;
        var problemDetailsResponse = new ProblemDetailsResponse
        {
            Title = notFoundException.Message,
            StatusCode = statusCode,
        };
        await context.Response.WriteAsJsonAsync(problemDetailsResponse);
    }

    private static async Task HandleDefaultExceptionAsync(HttpContext context, Exception exception)
    {
        const int statusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.StatusCode = statusCode;
        var problemDetailsResponse = new ProblemDetailsResponse
        {
            Title = "An unexpected server error occured",
            StatusCode = statusCode,
        };
        await context.Response.WriteAsJsonAsync(problemDetailsResponse);
    }
}