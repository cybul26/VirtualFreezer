using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.Shared.Infrastructure.Auth;
using VirtualFreezer.Shared.Infrastructure.Contexts;
using VirtualFreezer.Shared.Infrastructure.Exceptions;
using VirtualFreezer.Shared.Infrastructure.FastEndpoints;
using VirtualFreezer.Shared.Infrastructure.Mailing;
using VirtualFreezer.Shared.Infrastructure.Observability.Logging;
using VirtualFreezer.Shared.Infrastructure.Observability.Logging.Middlewares;
using VirtualFreezer.Shared.Infrastructure.Security;
using VirtualFreezer.Shared.Infrastructure.Serialization;
using VirtualFreezer.Shared.Infrastructure.Transactions;

namespace VirtualFreezer.Shared.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder AddMicroFramework(this WebApplicationBuilder builder,
        Action<RequestLoggingMappingConfiguration>?
            configureRequestLoggingMapping = null)
    {
        builder
            .AddLogging()
            .Services
            .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
            .AddTransactions()
            .AddErrorHandling()
            .AddEndpoints()
            .AddSwaggerDoc()
            .AddLogger(builder.Configuration, configureRequestLoggingMapping)
            .AddAuth()
            .AddContexts()
            .AddHttpContextAccessor()
            .AddSecurity()
            .AddMailing(builder.Configuration);


        return builder;
    }

    public static WebApplication UseMicroFramework(this WebApplication app)
    {
        app.UseContextLogger();
        app.UseErrorHandler();
        app.UseContexts();
        app.UseAuth();
        app.UseRequestLogging();
        app.UseTransactions();
        app.UseEndpoints();
        app.UseOpenApi();
        app.UseSwaggerUi3(c => { c.ConfigureDefaults(); });

        return app;
    }

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }

    public static T BindOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.BindOptions<T>(sectionName);
    }
}