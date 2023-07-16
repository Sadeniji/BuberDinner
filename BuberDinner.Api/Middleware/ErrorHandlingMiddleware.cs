using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }   

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;  // 500 if unexpected
        var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request." });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;
        return httpContext.Response.WriteAsync(result);
    }
}