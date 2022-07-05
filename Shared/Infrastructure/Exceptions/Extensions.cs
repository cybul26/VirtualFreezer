using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Mappers;

namespace VirtualFreezer.Shared.Infrastructure.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddTransient<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddTransient<ErrorHandlerMiddleware>();

        return services;
    }


    public static IApplicationBuilder UseErrorHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}