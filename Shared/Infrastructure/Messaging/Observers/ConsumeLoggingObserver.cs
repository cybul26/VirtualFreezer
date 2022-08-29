using MassTransit;
using Microsoft.Extensions.Logging;

namespace VirtualFreezer.Shared.Infrastructure.Messaging.Observers;

public class ConsumeLoggingObserver : IConsumeObserver
{
    private readonly ILogger<ConsumeLoggingObserver> _logger;

    public ConsumeLoggingObserver(ILogger<ConsumeLoggingObserver> logger)
    {
        _logger = logger;
    }

    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        _logger.LogInformation("Consuming message: {@Message} with id: {MessageId} and correlation id: {CorrelationId}",
            context.Message,
            context.MessageId, context.Headers.GetCorrelationId());
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        var messageId = context.MessageId;
        _logger.LogInformation("Consumed message with id: {MessageId} and correlation id: {CorrelationId} successfully", messageId,
            context.Headers.GetCorrelationId());
        return Task.CompletedTask;
    }

    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        _logger.LogError(exception,
            "Error while consuming message with id: {MessageId}. Correlation id: {CorrelationId}", context.MessageId,
            context.Headers.GetCorrelationId());
        return Task.CompletedTask;
    }
}