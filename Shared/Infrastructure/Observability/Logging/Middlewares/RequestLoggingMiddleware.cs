using System.Text;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using VirtualFreezer.Shared.Infrastructure.Contexts;
using VirtualFreezer.Shared.Infrastructure.Serialization;

namespace VirtualFreezer.Shared.Infrastructure.Observability.Logging.Middlewares;

internal class RequestLoggingMiddleware : IMiddleware
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly RequestLoggingMappingConfiguration _mappingConfiguration;
    private readonly IContextProvider _contextProvider;

    public RequestLoggingMiddleware(IJsonSerializer jsonSerializer, ILogger<RequestLoggingMiddleware> logger,
        RequestLoggingMappingConfiguration mappingConfiguration, IContextProvider contextProvider)
    {
        _jsonSerializer = jsonSerializer;
        _logger = logger;
        _mappingConfiguration = mappingConfiguration;
        _contextProvider = contextProvider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        if (endpoint is null)
        {
            await next(context);
            return;
        }

        var endpointDefinition = endpoint!.Metadata.GetMetadata<EndpointDefinition>();
        if (endpointDefinition is null)
        {
            await next(context);
            return;
        }

        var requestObject = await ReadRequestObjectAsync(context, endpointDefinition.ReqDtoType);


        var applicationContext = _contextProvider.Current();
        _logger.LogInformation(
            "Processing {@RequestName}: {@Request}. [Trace ID: {TraceId}, Correlation ID: {CorrelationId}, Causation ID: {CausationId}, User ID: {UserId}]",
            endpointDefinition.EndpointType.Name, requestObject ?? string.Empty, applicationContext.TraceId,
            applicationContext.CorrelationId,
            applicationContext.CausationId, applicationContext.UserId ?? string.Empty);

        await next(context);
    }

    private async ValueTask<object?> ReadRequestObjectAsync(HttpContext context, Type typeOfRequestDto)
    {
        if (typeOfRequestDto == typeof(EmptyRequest))
        {
            return new EmptyRequest();
        }


        context.Request.EnableBuffering();

        using var reader = new StreamReader(
            context.Request.Body,
            Encoding.UTF8,
            false,
            leaveOpen: true);

        var strRequestBody = await reader.ReadToEndAsync();
        var request = string.IsNullOrEmpty(strRequestBody)
            ? new EmptyRequest()
            : _jsonSerializer.Deserialize(strRequestBody, typeOfRequestDto!);

        var mappedRequest = _mappingConfiguration.MapRequestToLog(request);

        // IMPORTANT: Reset the request body stream position so the next middleware can read it
        context.Request.Body.Position = 0;
        return mappedRequest;
    }
}