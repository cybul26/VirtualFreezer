using MassTransit;
using Microsoft.Extensions.Logging;
using VirtualFreezer.Shared.Infrastructure.Contexts;

namespace VirtualFreezer.Shared.Infrastructure.Messaging;

internal class PublishMessageCorrelationIdFilter<T> : IFilter<PublishContext<T>>
    where T : class
{
    private readonly ILogger<T> _logger;
    private readonly IContextProvider _context;

    public PublishMessageCorrelationIdFilter(ILogger<T> logger, IContextProvider context)
    {
        _logger = logger;
        _context = context;
    }

    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        var correlationId = _context.Current().CorrelationId;
        context.Headers.SetCorrelationId(correlationId);
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}