using NftApi.Middleware;

namespace NftApi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder iddleware1(
        this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandleMiddleware>();
}