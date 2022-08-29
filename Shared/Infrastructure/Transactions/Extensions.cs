using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualFreezer.Shared.Infrastructure.Transactions;

internal static class Extensions
{
    public static IServiceCollection AddTransactions(this IServiceCollection services)
    {
        services.AddScoped<TransactionsMiddleware>();
        return services;
    }

    public static WebApplication UseTransactions(this WebApplication app)
    {
        app.UseMiddleware<TransactionsMiddleware>();
        return app;
    }
}