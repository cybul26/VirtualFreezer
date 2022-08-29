using MassTransit;
using Microsoft.Extensions.Logging;

namespace VirtualFreezer.Shared.Infrastructure.Messaging.Observers;

public class PublishLoggingObserver : IPublishObserver
{
    private readonly ILogger<PublishLoggingObserver> _logger;

    public PublishLoggingObserver(ILogger<PublishLoggingObserver> logger)
    {
        _logger = logger;
    }

    public Task PrePublish<T>(PublishContext<T> context) where T : class
    {
        _logger.LogInformation(
            "Publishing message: {@Message} with correlation id: {CorrelationId} and id: {MessageId}...",
            context.Message,
            context.Headers.GetCorrelationId(), context.MessageId);
        return Task.CompletedTask;
    }

    public Task PostPublish<T>(PublishContext<T> context) where T : class
        => Task.CompletedTask;

    public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
    {
        _logger.LogError(exception,
            "Publishing message with correlation id: {CorrelationId} and id: {MessageId} failed",
            context.Headers.GetCorrelationId(), context.MessageId);
        return Task.CompletedTask;
    }
}