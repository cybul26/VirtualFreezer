namespace VirtualFreezer.Shared.Infrastructure.Observability.Logging.Middlewares;

public class RequestLoggingMappingConfiguration
{
    public Func<object?, object?> MapRequestToLog = o => o;
}