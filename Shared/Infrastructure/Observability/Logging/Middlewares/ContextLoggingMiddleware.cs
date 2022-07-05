using Microsoft.AspNetCore.Http;
using Serilog.Context;
using VirtualFreezer.Shared.Infrastructure.Contexts;

namespace VirtualFreezer.Shared.Infrastructure.Observability.Logging.Middlewares;

internal sealed class ContextLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.GetCorrelationId()))
        {
            await next(httpContext);
        }
    }
}