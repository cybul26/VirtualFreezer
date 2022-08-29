using System.Diagnostics;
using MassTransit;
using Serilog;
using VirtualFreezer.Shared.Infrastructure.Contexts;
using VirtualFreezer.Shared.Infrastructure.Contexts.Accessors;
using LogContext = Serilog.Context.LogContext;

namespace VirtualFreezer.Shared.Infrastructure.Messaging.Filters;

public class ConsumeMessageCorrelationIdFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly IContextAccessor _contextAccessor;

    public ConsumeMessageCorrelationIdFilter(IContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var correlationId = context.Headers.GetCorrelationId();
        if (string.IsNullOrEmpty(correlationId))
        {
            await next.Send(context);
            return;
        }

        _contextAccessor.Context = new Context(Activity.Current?.Id ?? ActivityTraceId.CreateRandom().ToString(),
            string.Empty, correlationId, context.MessageId?.ToString("N"));

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next.Send(context);
        }
    }

    public void Probe(ProbeContext context)
    {
    }
}