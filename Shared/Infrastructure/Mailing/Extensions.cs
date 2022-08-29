using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace VirtualFreezer.Shared.Infrastructure.Mailing;

internal static class Extensions
{
    private const string SendGridSectionName = "sendGrid";
    public static IServiceCollection AddMailing(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SendGridSectionName);
        if (!section.Exists())
        {
            return services;    
        }

        var options = services.BindOptions<SendGridOptions>(SendGridSectionName);
        services.AddSingleton(options);
        services.AddSendGrid(o =>
        {
            o.ApiKey = options.ApiKey;
        });

        return services;
    }
}