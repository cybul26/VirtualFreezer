using Yarp.ReverseProxy.Transforms;

const string correlationIdKey = "correlation-id";
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(transform =>
        {
            var correlationId = Guid.NewGuid().ToString("N");
            transform.ProxyRequest.Headers.TryAddWithoutValidation(correlationIdKey, correlationId);

            return ValueTask.CompletedTask;
        });
    });
var app = builder.Build();

app.MapGet("/", () => "Gateway");
app.MapReverseProxy();

app.Run();