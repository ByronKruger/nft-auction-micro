
using System.Net;
using System.Text.Json;

namespace NftApi.Middleware;

public class ErrorHandleMiddleware : IMiddleware
{
    private readonly IHostEnvironment _env;
    // private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandleMiddleware(IHostEnvironment env, ILogger<ErrorHandleMiddleware> logger)//, RequestDelegate next)
    {
        _env = env;
        _logger = logger;
        // _next = next;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment() 
                ? new ApiException(HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiException(HttpStatusCode.InternalServerError, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseFactoryActivatedMiddleware(
        this IApplicationBuilder app) => app.UseMiddleware<ErrorHandleMiddleware>();
}
