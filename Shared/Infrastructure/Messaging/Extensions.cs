using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.Shared.Infrastructure.Messaging.Filters;
using VirtualFreezer.Shared.Infrastructure.Messaging.Observers;

namespace VirtualFreezer.Shared.Infrastructure.Messaging;

public static class Extensions
{
    private const string SectionName = "rabbit";
    private const string CorrelationidHeader = "correlation-id";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration,
        params Type[] consumers)
    {
        var section = configuration.GetSection(SectionName);
        if (!section.Exists())
        {
            return services;
        }

        var options = configuration.BindOptions<RabbitOptions>(SectionName);
        services.AddMassTransit(config =>
        {
            config.AddConsumers(consumers);
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host, "/", h =>
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });

                cfg.ConfigureEndpoints(context);
                cfg.UsePublishFilter(typeof(PublishMessageCorrelationIdFilter<>), context);
                cfg.UseConsumeFilter(typeof(ConsumeMessageCorrelationIdFilter<>), context);
            });
        });
        services.AddConsumeObserver<ConsumeLoggingObserver>();
        services.AddPublishObserver<PublishLoggingObserver>();
        return services;
    }

    public static string GetCorrelationId(this Headers headers)
        => headers.Get(CorrelationidHeader, string.Empty) ?? string.Empty;

    public static void SetCorrelationId(this SendHeaders headers, string correlationId)
        => headers.Set(CorrelationidHeader, correlationId);
}